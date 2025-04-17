using System;
using System.Collections.Generic;

namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    [Serializable]
    public class PlayerInventorySaveData
    {
        public int selectedSlotIndex;
        public List<ItemSaveData> slots;

        public PlayerInventorySaveData(int selectedSlotIndex, List<ItemSaveData> slots)
        {
            this.selectedSlotIndex = selectedSlotIndex;
            this.slots = slots;
        }
    }
}