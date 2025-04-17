namespace _Quarantine.Code.Utils.FiniteStateMachine
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}