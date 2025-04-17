using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameStates;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Root
{
    public class Game : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        public void Run(GameRunningMode mode, IGameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
            
            switch (mode)
            {
                case GameRunningMode.PlayerAccessMode:
                    _gameStateMachine.Enter<MainMenuState>();
                    break;
                case GameRunningMode.ActiveSceneDebugMode:
                    _gameStateMachine.Enter<ProgressLoadingState>();
                    break;
            }
        }

        private void Update()
        {
            _gameStateMachine.Update();
        }
    }
}