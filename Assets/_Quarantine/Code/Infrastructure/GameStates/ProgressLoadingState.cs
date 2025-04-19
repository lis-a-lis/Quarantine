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

        public ProgressLoadingState(IGameStateMachine gameStateMachine, IProgressSaveLoadService progressSaveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressSaveLoadService = progressSaveLoadService;
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
            if (progress == null)
                Debug.Log("Progress is null!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            
            _gameStateMachine.Enter<SetupState, GameProgress>(progress);
        }
    }
}