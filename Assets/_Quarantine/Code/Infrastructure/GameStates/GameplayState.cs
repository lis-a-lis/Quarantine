using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class GameplayState : IUpdatableState
    {
        public void Enter()
        {
            Debug.Log("Gameplay State Enter");
        }

        public void Exit()
        {
        }

        public void Update()
        {
            
        }
    }
}