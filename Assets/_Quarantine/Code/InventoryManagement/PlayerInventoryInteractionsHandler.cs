using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.InventoryManagement
{
    [RequireComponent(typeof(PlayerInventory))]
    [RequireComponent(typeof(PlayerViewRaycaster))]
    public class PlayerInventoryInteractionsHandler : MonoBehaviour, IInventoryInteractionsHandler
    {
        [SerializeField] private LayerMask _itemsLayerMask;
        [SerializeField] private PlayerHandsView _playerHands;
        [SerializeField] private float _slotSelectionDuration = 1f;
        [SerializeField] private float _throwForceMultiplier = 5;

        private PlayerViewRaycaster _playerViewRaycaster;
        private PlayerInventory _inventory;
        private bool _isSwitching;

        public event Action<Item> ItemAppearInHands;
        public event Action ItemDisappearInHands;

        private void Awake()
        {
            _inventory = GetComponent<PlayerInventory>();
            _playerViewRaycaster = GetComponent<PlayerViewRaycaster>();
        }

        public void Initialize()
        {
            if (_inventory.IsSelectedSlotFilled)
            {
                _playerHands.SetItemInHands(_inventory.SelectedItem);
                _playerHands.RunItemAppearAnimation(0);
            }
        }

        private async UniTaskVoid SwitchSlot(int slotIndex)
        {
            _isSwitching = true;

            if (_playerHands.IsItemInHands)
            {
                _playerHands.RunItemDisappearAnimation(_slotSelectionDuration);
                
                ItemDisappearInHands?.Invoke();

                await UniTask.WaitForSeconds(_slotSelectionDuration);

                _playerHands.ClearItemInHands();
            }

            _inventory.SelectSlot(slotIndex);

            if (_inventory.IsSelectedSlotFilled)
            {
                _playerHands.SetItemInHands(_inventory.SelectedItem);
                _playerHands.RunItemAppearAnimation(_slotSelectionDuration);

                await UniTask.WaitForSeconds(_slotSelectionDuration);
                
                ItemAppearInHands?.Invoke(_inventory.SelectedItem);
            }

            _isSwitching = false;
        }

        private async UniTaskVoid AppearPickedItem()
        {
            _isSwitching = true;

            _playerHands.SetItemInHands(_inventory.SelectedItem);
            _playerHands.RunItemAppearAnimation(_slotSelectionDuration);

            await UniTask.WaitForSeconds(_slotSelectionDuration);

            ItemAppearInHands?.Invoke(_inventory.SelectedItem);
            
            _isSwitching = false;
        }

        public void SelectSlot(int slotIndex)
        {
            if (_isSwitching)
                return;

            SwitchSlot(slotIndex).Forget();
        }

        public void PickUpItem()
        {
            if (!_playerViewRaycaster.RaycastTrigger(out Item item, _itemsLayerMask))
                return;

            bool isItemPickedToSelectedSlot = _inventory.IsSelectedSlotEmpty;
            
            Debug.Log($"picked up item {item.Id}");
            if (_inventory.AddItem(item))
            {
                item.gameObject.SetActive(false);
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.transform.SetParent(transform);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                if (isItemPickedToSelectedSlot && _inventory.IsSelectedSlotFilled)
                    AppearPickedItem().Forget();
            }
        }

        public void DropItem()
        {
            if (_isSwitching)
                return;

            if (!_playerHands.IsItemInHands)
                return;

            if (!_inventory.DropSelectedItem(out Item droppedItem))
                return;

            ItemDisappearInHands?.Invoke();
            _playerHands.ClearItemInHands();
            droppedItem.gameObject.SetActive(true);
            droppedItem.transform.SetParent(null);
            Rigidbody itemRigidbody = droppedItem.GetComponent<Rigidbody>();
            itemRigidbody.isKinematic = false;
            itemRigidbody.AddForce(_playerViewRaycaster.ViewDirection * _throwForceMultiplier, ForceMode.Impulse);
        }

        public void PlaceItem()
        {
            if (_isSwitching)
                return;

            if (!_playerHands.IsItemInHands)
                return;

            if (_inventory.IsSelectedSlotEmpty)
                return;

            if (_playerViewRaycaster.Raycast(out RaycastHit hit))
            {
                if (hit.normal != Vector3.up)
                    return;
                
                ItemDisappearInHands?.Invoke();
                _inventory.DropSelectedItem(out Item droppedItem);
                _playerHands.ClearItemInHands();
                droppedItem.transform.position = hit.point;
                droppedItem.transform.rotation = Quaternion.identity;
                droppedItem.gameObject.SetActive(true);
            }
        }
    }
}