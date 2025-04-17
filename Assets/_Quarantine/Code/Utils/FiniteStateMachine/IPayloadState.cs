namespace _Quarantine.Code.Utils.FiniteStateMachine
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}