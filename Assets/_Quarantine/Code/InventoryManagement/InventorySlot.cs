using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.InventoryManagement
{
    public class InventorySlot
    {
        public Item Item { get; set; }
        
        public bool IsFilled => Item != null;
        public bool IsEmpty => Item == null;
    }
}