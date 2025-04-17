namespace _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States
{
    public interface IState : IExitState
    {
        public void Enter();
    }
}