using System;

namespace _Quarantine.Code.InventoryManagement
{
    public interface IObservablePlayerInventory
    {
        public event Action<int> SlotSelected;
        public event Action<int> SlotUnselected;
        public event Action<int> ItemAdded;
        public event Action<int> ItemRemoved;

        public int SlotsAmount { get; }
        public bool IsSlotSelected {get; }
        public bool IsSelectedSlotFilled { get; }
        public bool IsSelectedSlotEmpty { get; }
        public bool TryGetItemIdBySlotIndex(int slotIndex, out string id);
    }
}