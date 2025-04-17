namespace _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States
{
    public interface IPayloadState<TPayload> : IExitState
    {
        public void Enter(TPayload payload);
    }
}