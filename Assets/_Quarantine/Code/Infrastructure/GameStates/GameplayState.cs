using UnityEngine;
using _Quarantine.Code.UI.HUD.PlayerInventory;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.Services.SceneLoading;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class GameplayState : IUpdatableState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameProgressSaveService _saveLoadService;
        private readonly ISceneLoader _sceneLoader;
        private readonly UIRoot _uiRoot;
        public GameplayState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            IGameProgressSaveService saveLoadService, UIRoot uiRoot)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
            _uiRoot = uiRoot;
        }
        
        public void Enter()
        {
            _uiRoot.HideLoadingScreen();
            
            Debug.Log("Gameplay State Enter");
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.M))
                BackToMenu();
        }

        private void BackToMenu()
        {
            _uiRoot.ShowLoadingScreen();
                
            _saveLoadService.Save();
            
            _saveLoadService.ClearSavableEntities();

            var hud = Object.FindFirstObjectByType<InventoryHUDPresenter>();
                
            Debug.Log(hud);
                
            Object.Destroy(hud.gameObject);
                
            _sceneLoader.LoadScene(Scenes.Menu, 
                () => true,
                f => Debug.Log("back to menu"),
                () => _gameStateMachine.Enter<MainMenuState>());
        }
    }
}