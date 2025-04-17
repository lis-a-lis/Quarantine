namespace _Quarantine.Code.InventoryManagement
{
    public interface IInventoryInteractionsHandler
    {
        public void PickUpItem();
        public void DropItem();
        public void PlaceItem();
    }
}