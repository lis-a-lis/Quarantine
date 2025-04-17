using _Quarantine.Code.Infrastructure.Root.UI;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.UI
{
    public class UIRootFactory
    {
        private const string PrefabPath = "Root/UIRoot";

        public UIRoot Create()
        {
            UIRoot uiRoot = Object.Instantiate(Resources.Load<UIRoot>(PrefabPath));
            Object.DontDestroyOnLoad(uiRoot);
            return uiRoot;
        }
    }
}