using _Quarantine.Code.InventorySystem.Items.Behavior;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem
{
    public interface IInventory
    {
        public void Initialize(Vector2Int capacity);
        public bool HasItem(string itemName, int requiredAmount, out int foundedAmount);
        public OldItem GetItem(Vector2Int position);
        public void AddItem(OldItem oldItem);
        public void RemoveItem(Vector2Int position);  
    }
}