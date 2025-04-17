using UnityEngine;
using _Quarantine.Code.UI.MainMenu;
using _Quarantine.Code.Infrastructure.Root.UI;

namespace _Quarantine.Code.Infrastructure.Services.UI
{
    public class MainMenuFactory
    {
        private const string PrefabPath = "UI/MainMenu";
        
        private readonly UIRoot _uiRoot;

        public MainMenuFactory(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public MainMenu Create()
        {
            MainMenu menu = Object.Instantiate(Resources.Load<MainMenu>(PrefabPath));
            _uiRoot.AttachToGroup(menu.gameObject, "MENU");
            return menu;
        }
    }
}