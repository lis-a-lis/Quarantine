namespace _Quarantine.Code.Utils.FiniteStateMachine
{
    public interface IUpdatableState : IState
    {
        public void Update();
    }
}