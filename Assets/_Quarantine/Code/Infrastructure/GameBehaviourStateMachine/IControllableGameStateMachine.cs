using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameBehaviourStateMachine
{
    public interface IControllableGameStateMachine : IGameStateMachine
    {
        public void AddState<TState>(IExitState state);
    }
}