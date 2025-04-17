using System;
using UnityEngine;
using _Quarantine.Code.UI.MainMenu;
using _Quarantine.Code.Infrastructure.Services.UI;
using _Quarantine.Code.Infrastructure.Services.SceneLoading;
using _Quarantine.Code.Infrastructure.GameRequests;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly MainMenuFactory _menuFactory;
        private MainMenu _menu;

        public MainMenuState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, MainMenuFactory mainMenuFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _menuFactory = mainMenuFactory;
        }

        public void Enter()
        {
            Debug.Log("Menu State Enter");

            _menu = _menuFactory.Create();

            _menu.RequestSended += HandleMainMenuRequest;
        }

        public void Exit()
        {
            _menu.RequestSended -= HandleMainMenuRequest;
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
            _sceneLoader.LoadScene(Scenes.Gameplay,
                () => true, 
                (progress) => Debug.Log("LOADING"),
                OnGameplaySceneLoaded);
        }

        private void OnGameplaySceneLoaded()
        {
            _gameStateMachine.Enter<ProgressLoadingState>();
            _menu.gameObject.SetActive(false);
        }

        private void Quit()
        {
            
        }
    }
}