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
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
            LoadProgress();
        }

        private void LoadProgress()
        {
            _progressSaveLoadService.Load(OnProgressLoaded).Forget();
        }

        private void OnProgressLoaded(GameProgress progress)
        {
            _gameStateMachine.Enter<SetupState, GameProgress>(progress);
        }
    }
}