namespace _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States
{
    public interface IUpdatableState : IState
    {
        public void Update();
    }
}