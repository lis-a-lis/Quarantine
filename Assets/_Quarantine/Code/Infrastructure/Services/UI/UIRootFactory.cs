using UnityEngine;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;

namespace _Quarantine.Code.Infrastructure.Services.UI
{
    public class UIRootFactory
    {
        private const string PrefabPath = "Root/UIRoot";

        private readonly IAssetsProvider _assetsProvider;
        
        public UIRootFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public UIRoot Create()
        {
            UIRoot uiRoot = Object.Instantiate(_assetsProvider.LoadPrefab<UIRoot>(PrefabPath));
            Object.DontDestroyOnLoad(uiRoot);
            return uiRoot;
        }
    }
}