using UnityEngine;
using _Quarantine.Code.Items.Implementation;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.InventoryManagement
{
    [RequireComponent(typeof(PlayerInventory))]
    public class PlayerInventoryInteractionsHandler : MonoBehaviour, IInventoryInteractionsHandler
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private PlayerHandsView _playerHands;
        [SerializeField] private float _pickUpDistance = 2;
        [SerializeField] private float _slotSelectionDuration = 1f;
        
        private PlayerInventory _inventory;
        private bool _isSwitching;

        private void Awake()
        {
            _inventory = GetComponent<PlayerInventory>();
        }

        public void Initialize()
        {
            if (_inventory.IsSelectedSlotFilled)
            {
                _inventory.TryGetItem(_inventory.SelectedSlotIndex, out var item);
                _playerHands.SetItemInHands(item);
                _playerHands.RunItemAppearAnimation(0);
            }
        }
        
        private async UniTaskVoid SwitchSlot(int slotIndex)
        {
            _isSwitching = true;

            if (_playerHands.IsItemInHands)
            {
                _playerHands.RunItemDisappearAnimation(_slotSelectionDuration);
                
                await UniTask.WaitForSeconds(_slotSelectionDuration);
                
                _playerHands.ClearItemInHands();
            }
            
            _inventory.SelectSlot(slotIndex);

            if (_inventory.IsSelectedSlotFilled)
            {
                _inventory.TryGetItem(slotIndex, out var item);
                _playerHands.SetItemInHands(item);
                _playerHands.RunItemAppearAnimation(_slotSelectionDuration);
                
                await UniTask.WaitForSeconds(_slotSelectionDuration);
            }

            _isSwitching = false;
        }

        private async UniTaskVoid AppearPickedItem()
        {
            _isSwitching = true;
            
            _inventory.TryGetItem(_inventory.SelectedSlotIndex, out var item);
            _playerHands.SetItemInHands(item);
            _playerHands.RunItemAppearAnimation(_slotSelectionDuration);
                
            await UniTask.WaitForSeconds(_slotSelectionDuration);
            
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
            if (!Physics.Raycast(_playerHead.position + _playerHead.forward * 0.7f, 
                    _playerHead.forward, out var hit, _pickUpDistance))
                return;
            
            if (hit.collider.gameObject.TryGetComponent(out Item item))
            {
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
        }

        public void DropItem()
        {
            if (_isSwitching)
                return;
            
            if (!_playerHands.IsItemInHands)
                return;
            
            if (!_inventory.DropSelectedItem(out Item droppedItem))
                return;
            
            _playerHands.ClearItemInHands();
            droppedItem.gameObject.SetActive(true);
            droppedItem.transform.SetParent(null);
            Rigidbody itemRigidbody = droppedItem.GetComponent<Rigidbody>();
            itemRigidbody.isKinematic = false;
            itemRigidbody.AddForce(_playerHead.forward * 10, ForceMode.Impulse);
        }

        public void PlaceItem()
        {
            if (!_inventory.IsSelectedSlotEmpty)
                return;
            
            if (Physics.Raycast(_playerHead.position, _playerHead.forward, out var hit, _pickUpDistance))
            {
                if (hit.normal != Vector3.up)
                    return;
                
                _inventory.DropSelectedItem(out Item droppedItem);
                droppedItem.transform.position = hit.point;
                droppedItem.transform.rotation = Quaternion.LookRotation(hit.normal);
                droppedItem.gameObject.SetActive(true);
            }
        }
    }
}