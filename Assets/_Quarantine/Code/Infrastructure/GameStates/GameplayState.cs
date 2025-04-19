using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.Services.SceneLoading;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class GameplayState : IPayloadState<List<ISaveLoadEntity>>, IUpdatableState
    {
        private InputSystem_Actions _input;
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IProgressSaveLoadService _progressSaveLoadService;
        private UIRoot _uiRoot;
        private List<ISaveLoadEntity> _entities;

        public GameplayState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            IProgressSaveLoadService progressSaveLoadService, UIRoot uiRoot)
        {
            _gameStateMachine = gameStateMachine;
            _progressSaveLoadService = progressSaveLoadService;
            _uiRoot = uiRoot;
            _sceneLoader = sceneLoader;
            //_input = new InputSystem_Actions();
        }
        
        private void OnMenuButtonPressed(InputAction.CallbackContext obj)
        {
            Debug.Log("OnMenuButtonPressed");
            
            _sceneLoader.LoadScene(Scenes.Menu, 
                () => true,
                f => Debug.Log("back to menu"),
                () => _gameStateMachine.Enter<MainMenuState>());
        }

        private void OnSaveButtonPressed(InputAction.CallbackContext obj)
        {
            Debug.Log("Save progress..");
            
            ISavableEntitiesVisitor visitor = new SavableEntitiesVisitor();

            foreach (var entity in _entities)
            {
                entity.AcceptSave(visitor);
            }
            
            _progressSaveLoadService.Save(visitor.GameProgress,
                () => Debug.Log("Progress saved!")).Forget();

        }

        public void Enter(List<ISaveLoadEntity> payload)
        {
            _entities = payload;

            _uiRoot.HideLoadingScreen();
            /*_input.Player.Menu.performed += OnMenuButtonPressed;
            _input.Player.Save.performed += OnSaveButtonPressed; 
            */
            
            Debug.Log("Gameplay State Enter");
        }

        public void Exit()
        {
            /*_input.Player.Menu.performed -= OnMenuButtonPressed;
            _input.Player.Save.performed -= OnSaveButtonPressed;*/
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("OnMenuButtonPressed");
            
                _uiRoot.ShowLoadingScreen();
                
                _sceneLoader.LoadScene(Scenes.Menu, 
                    () => true,
                    f => Debug.Log("back to menu"),
                    () => _gameStateMachine.Enter<MainMenuState>());
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Save progress..");
            
                ISavableEntitiesVisitor visitor = new SavableEntitiesVisitor();

                foreach (var entity in _entities)
                {
                    entity.AcceptSave(visitor);
                }
                
                Debug.Log(visitor.GameProgress.player.transform.playerPosition);
            
                _progressSaveLoadService.Save(visitor.GameProgress,
                    () => Debug.Log("Progress saved!")).Forget();
            }
        }
    }
}