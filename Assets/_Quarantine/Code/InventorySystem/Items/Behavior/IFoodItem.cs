using _Quarantine.Code.InventorySystem.Items.Configuration;

namespace _Quarantine.Code.InventorySystem.Items.Behavior
{
    public interface IFoodItem
    {
        public void Initialize(FoodConfig config);
    }
}