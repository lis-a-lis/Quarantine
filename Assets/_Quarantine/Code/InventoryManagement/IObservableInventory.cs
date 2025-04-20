using System;

namespace _Quarantine.Code.InventoryManagement
{
    public interface IObservableInventory
    {
        public event Action<int> ItemAdded;
        public event Action<int> ItemRemoved;
        public event Action<int> SelectedSlotChanged;
        
        public int SlotsAmount { get; }
        public bool IsSlotSelected { get; }
        public bool IsSelectedSlotFilled { get; }
        public int SelectedSlotIndex { get; }
        public bool SlotSelectionStatus(int slotIndex);

        public bool TryGetItemData(int slotIndex, out string itemId, out float itemDurability);
    }
}