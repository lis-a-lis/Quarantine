using _Quarantine.Code.Items.Implementation;
using UnityEngine;

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

        public void PickUpItem()
        {
            if (!Physics.Raycast(_playerHead.position, _playerHead.forward, out var hit, _pickUpDistance))
                return;
            
            if (hit.collider.gameObject.TryGetComponent(out Item item))
            {
                if (_inventory.AddItem(item))
                    item.gameObject.SetActive(false);
            }
        }

        public void DropItem()
        {
            if (!_inventory.DropSelectedItem(out Item droppedItem))
                return;
            
            droppedItem.transform.position = _playerHead.position;
            droppedItem.gameObject.SetActive(true);
        }

        public void PlaceItem()
        {
            if (!_inventory.IsItemSelected)
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