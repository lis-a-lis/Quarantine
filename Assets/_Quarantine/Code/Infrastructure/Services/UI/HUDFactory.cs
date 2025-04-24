using UnityEngine;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.UI.HUD.PlayerInventory;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;

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

        public InventoryHUDPresenter CreateInventoryHUD()
        {
            InventoryHUDPresenter prefab = 
                _assetsProvider.LoadPrefab<InventoryHUDPresenter>(InventoryHUDPrefabPath);
            
            InventoryHUDPresenter inventoryHUD = Object.Instantiate(prefab);
            
            _uiRoot.Attach(inventoryHUD.gameObject);

            return inventoryHUD;
        }
    }
}