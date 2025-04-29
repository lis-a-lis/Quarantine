using System;
using UnityEngine;
using UnityEditor;
using _Quarantine.Code.UI.MainMenu;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services.UI;
using _Quarantine.Code.Infrastructure.GameRequests;
using _Quarantine.Code.Infrastructure.Services.SceneLoading;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly MainMenu _menu;
        private readonly UIRoot _uiRoot;

        public MainMenuState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            MainMenuFactory mainMenuFactory, UIRoot uiRoot)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiRoot = uiRoot;
            _menu = mainMenuFactory.Create();
            _menu.gameObject.SetActive(false);
        }

        public void Enter()
        {
            Debug.Log("Menu State Enter");
            
            _uiRoot.HideLoadingScreen();
            _menu.gameObject.SetActive(true);
            _menu.RequestSent += HandleMainMenuRequest;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Exit()
        {
            _menu.RequestSent -= HandleMainMenuRequest;
            _menu.gameObject.SetActive(false);
        }

        private void HandleMainMenuRequest(MainMenuRequest request)
        {
            switch (request)
            {
                case MainMenuRequest.Play:
                    Play();
                    break;
                case MainMenuRequest.Quit:
                    Quit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(request), request, null);
            }
        }

        private void Play()
        {
            Debug.Log("Play menu state");
            
            _uiRoot.ShowLoadingScreen();
            
            _sceneLoader.LoadScene(Scenes.Gameplay,
                () => true, 
                (progress) => Debug.Log("LOADING"),
                OnGameplaySceneLoaded);
        }

        private void OnGameplaySceneLoaded()
        {
            _menu.gameObject.SetActive(false);
            _gameStateMachine.Enter<ProgressLoadingState>();
        }

        private void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif            
        }
    }
}