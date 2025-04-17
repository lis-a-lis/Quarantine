using _Quarantine.Code.InventorySystem.Items.Configuration;

namespace _Quarantine.Code.InventorySystem.Items.Behavior
{
    public interface IAmmoItem
    {
        public void Initialize(AmmoConfig config);
    }
}