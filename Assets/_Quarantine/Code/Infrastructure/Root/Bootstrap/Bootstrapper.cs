using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services;
using _Quarantine.Code.Infrastructure.Services.SceneLoading;
using _Quarantine.Code.Infrastructure.Services.UI;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Root.Bootstrap
{
    public class Bootstrapper : IBootstrapper
    {
        private ServiceContainer _container;
        private GameStateMachine _gameStateMachine;
        private Game _game;
        
        public Bootstrapper()
        {
            RegisterAll();
        }

        public void Run(GameRunningMode mode) =>
            _game.Run(mode, _gameStateMachine);

        private void RegisterAll()
        {
            RegisterServices();
            RegisterGame();
            RegisterGameStateMachine();
        }

        private void RegisterServices()
        {
            _container = new ServiceContainer();

            _container.Register<UIRootFactory>(new UIRootFactory());
            _container.Register<UIRoot>(_container.Get<UIRootFactory>().Create());
            _container.Register<MainMenuFactory>(new MainMenuFactory(_container.Get<UIRoot>()));
            //_container.Register<IItemDatabaseService>(new ItemDatabaseService());
            _container.Register<ISceneLoader>(new SceneLoader());
        }

        private void RegisterGame()
        {
            _game = new GameObject("[GAME]").AddComponent<Game>();
            Object.DontDestroyOnLoad(_game);
        }

        private void RegisterGameStateMachine()
        {
            /*_gameStateMachine = new GameStateMachine();
            
            _gameStateMachine.AddState<MainMenuState>(
                new MainMenuState(
                    _gameStateMachine, 
                    _container.Get<ISceneLoader>(),
                    _container.Get<MainMenuFactory>().Create()));
            
            _gameStateMachine.AddState<SetupState>(new SetupState(_gameStateMachine));
            
            _gameStateMachine.AddState<GameplayState>(new GameplayState());*/
        }
    }
}