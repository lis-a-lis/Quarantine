using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using _Quarantine.Code.UI.HUD.InventoryHUD;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.UI
{
    public class HUDFactory : IHUDFactory
    {
        private const string InventoryHUDPrefabPath = "UI/PlayerInventoryHUD";
        
        private readonly IAssetsProvider _assetsProvider;
        private readonly UIRoot _uiRoot;

        public HUDFactory(IAssetsProvider assetsProvider, UIRoot uiRoot)
        {
            _assetsProvider = assetsProvider;
            _uiRoot = uiRoot;
        }

        public PlayerInventoryHUDPresenter CreateInventoryHUD()
        {
            PlayerInventoryHUDPresenter prefab = 
                _assetsProvider.LoadPrefab<PlayerInventoryHUDPresenter>(InventoryHUDPrefabPath);
            
            PlayerInventoryHUDPresenter inventoryHUD = Object.Instantiate(prefab);
            
            _uiRoot.Attach(inventoryHUD.gameObject);

            return inventoryHUD;
        }
    }
}