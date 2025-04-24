using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameBehaviourStateMachine
{
    public class GameStateMachine : IControllableGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states = new();
        
        private IExitState _activeState;

        public void AddState<TState>(IExitState state)
        {
            Debug.Log($"{nameof(GameStateMachine)}: {typeof(TState).Name} state added");
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
            Debug.Log($"{nameof(GameStateMachine)}: " +
                      $"{(_activeState == null ? string.Empty : _activeState.GetType().Name + " exited\n")}" +
                      $"{typeof(TState).Name} entered");
            
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState =>
            _states[typeof(TState)] as TState;
    }
}