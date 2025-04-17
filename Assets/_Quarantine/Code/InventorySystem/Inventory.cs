using System;
using System.Collections.Generic;
using _Quarantine.Code.InventorySystem.Items.Behavior;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem
{

    
    
    public class Inventory : MonoBehaviour, IInventory, IObservableInventory
    {
        private List<OldItem> _items;
        
        public event Action<Vector2Int, int> OnItemAdded;
        public event Action<Vector2Int, int> OnItemRemoved;
        
        private void Awake()
        {
            _items = new List<OldItem>();
        }

        public void Initialize(Vector2Int capacity)
        {
            throw new NotImplementedException();
        }

        public bool HasItem(string itemName, int requiredAmount, out int foundedAmount)
        {
            throw new NotImplementedException();
        }

        public OldItem GetItem(Vector2Int position)
        {
            throw new NotImplementedException();
        }

        public void AddItem(OldItem oldItem)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(Vector2Int position)
        {
            throw new NotImplementedException();
        }
    }
}