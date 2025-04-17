using System;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem
{
    public interface IObservableInventory
    {
        public event Action<Vector2Int, int> OnItemAdded;
        public event Action<Vector2Int, int> OnItemRemoved;
    }
}