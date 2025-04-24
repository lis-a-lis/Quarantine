using _Quarantine.Code.UI.HUD.PlayerInventory;

namespace _Quarantine.Code.Infrastructure.Services.UI
{
    public interface IHUDFactory
    {
        public InventoryHUDPresenter CreateInventoryHUD();
    }
}