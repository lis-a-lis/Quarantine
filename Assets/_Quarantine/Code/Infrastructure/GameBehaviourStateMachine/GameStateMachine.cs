using System;
using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameBehaviourStateMachine
{
    public class GameStateMachine : IControllableGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states = new();
        
        private IExitState _activeState;

        public GameStateMachine()
        {
            
        }

        public void AddState<TState>(IExitState state)
        {
            
            Debug.Log($"Adding state: {typeof(TState).Name}");
            _states.Add(typeof(TState), state);
        }

        public void Enter<TState>() where TState : class, IState
        {   
            ChangeState<TState>().Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {   
            ChangeState<TState>().Enter(payload);
        }

        public void Update()
        {
            if (_activeState is IUpdatableState state)
                state.Update();
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState =>
            _states[typeof(TState)] as TState;
    }
}