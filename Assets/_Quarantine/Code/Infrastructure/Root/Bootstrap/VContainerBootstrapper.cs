using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameStates;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace _Quarantine.Code.Infrastructure.Root.Bootstrap
{
    public class VContainerBootstrapper : IBootstrapper
    {
        private const string GameRootObjectName = "[GAME]";
        
        private GameLifetimeScope _gameScope;
        private IControllableGameStateMachine _gameStateMachine;
        private Game _game;

        public VContainerBootstrapper()
        {
            InitializeGame();
            InitializeGameStateMachine();
        }
        
        public void Run(GameRunningMode mode)
        {
            _game.Run(mode, _gameStateMachine);
        }
        
        private void InitializeGame()
        {
            _game = new GameObject(GameRootObjectName).AddComponent<Game>();
            _gameScope = _game.AddComponent<GameLifetimeScope>();
            Object.DontDestroyOnLoad(_game);
        }
        
        private void InitializeGameStateMachine()
        {
            _gameStateMachine = _gameScope.Container.Resolve<IControllableGameStateMachine>();
            
            _gameStateMachine.AddState<MainMenuState>(_gameScope.Container.Resolve<MainMenuState>());
            
            _gameStateMachine.AddState<SetupState>(_gameScope.Container.Resolve<SetupState>());
            _gameStateMachine.AddState<LinearSetupState>(_gameScope.Container.Resolve<LinearSetupState>());
            
            _gameStateMachine.AddState<ProgressLoadingState>(_gameScope.Container.Resolve<ProgressLoadingState>());
            
            _gameStateMachine.AddState<GameplayState>(_gameScope.Container.Resolve<GameplayState>());
        }
    }
}