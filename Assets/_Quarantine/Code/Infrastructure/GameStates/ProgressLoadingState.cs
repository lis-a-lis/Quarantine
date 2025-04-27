using UnityEngine;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class ProgressLoadingState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IProgressSaveLoadService _progressSaveLoadService;
        private readonly IGameProgressSaveService _gameProgressSaveService;

        public ProgressLoadingState(IGameStateMachine gameStateMachine,
            IProgressSaveLoadService progressSaveLoadService,
            IGameProgressSaveService gameProgressSaveService)
        {
            _gameStateMachine = gameStateMachine;
            _progressSaveLoadService = progressSaveLoadService;
            _gameProgressSaveService = gameProgressSaveService;
        }
        
        public void Exit()
        {
            
        }

        public void Enter()
        {
            Debug.Log("Progress loading State Enter");
            
            LoadProgress();
        }

        private void LoadProgress()
        {
            _progressSaveLoadService.Load(OnProgressLoaded).Forget();
        }

        private void OnProgressLoaded(GameProgress progress)
        {
            Debug.Log("Progress loaded");
            
            _gameProgressSaveService.Initialize(progress);
            //_gameStateMachine.Enter<CoroutineBasedSetupState, GameProgress>(progress);
            _gameStateMachine.Enter<SetupState, GameProgress>(progress);
        }
    }
}