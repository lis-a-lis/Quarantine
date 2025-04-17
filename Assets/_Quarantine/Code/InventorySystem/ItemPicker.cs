using _Quarantine.Code.InventorySystem.Items.Behavior;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private Inventory _ownerInventory;
        [SerializeField] private Transform _ownerHead;
        [SerializeField] private float _pickUpDistance;
        [SerializeField] private LayerMask _itemsLayerMask;
        
        public void PickUpItem()
        {
            if (Physics.Raycast(_ownerHead.position, _ownerHead.forward, out RaycastHit hit, _pickUpDistance, _itemsLayerMask))
                _ownerInventory.AddItem(hit.collider.gameObject.GetComponent<OldItem>());
        }
    }
}