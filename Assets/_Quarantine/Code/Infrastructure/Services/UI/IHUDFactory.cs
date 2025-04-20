using _Quarantine.Code.UI.HUD.InventoryHUD;

namespace _Quarantine.Code.Infrastructure.Services.UI
{
    public interface IHUDFactory
    {
        public PlayerInventoryHUDPresenter CreateInventoryHUD();
    }
}