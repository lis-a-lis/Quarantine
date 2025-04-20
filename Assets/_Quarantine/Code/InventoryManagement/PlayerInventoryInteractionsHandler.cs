using UnityEngine;
using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.InventoryManagement
{
    [RequireComponent(typeof(Inventory))]
    public class PlayerInventoryInteractionsHandler : MonoBehaviour, IInventoryInteractionsHandler
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private float _pickUpDistance = 2;
        //[SerializeField] private LayerMask _itemsLayer;
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
        }

        public void SelectSlot(int slotIndex)
        {
            _inventory.SelectSlot(slotIndex);
        }

        public void PickUpItem()
        {
            if (!Physics.Raycast(_playerHead.position + _playerHead.forward * 0.7f, 
                    _playerHead.forward, out var hit, _pickUpDistance))
                return;
            
            if (hit.collider.gameObject.TryGetComponent(out Item item))
            {
                Debug.Log($"picked up item {item.Id}");
                if (_inventory.AddItem(item))
                    item.gameObject.SetActive(false);
            }
        }

        public void DropItem()
        {
            if (!_inventory.DropSelectedItem(out Item droppedItem))
                return;
            
            Rigidbody itemRigidbody = droppedItem.GetComponent<Rigidbody>();
            droppedItem.transform.SetParent(null);
            itemRigidbody.isKinematic = false;
            itemRigidbody.AddForce(_playerHead.forward * 10, ForceMode.Impulse);
            droppedItem.gameObject.SetActive(true);
        }

        public void PlaceItem()
        {
            if (!_inventory.IsSlotSelected)
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