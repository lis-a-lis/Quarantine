using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.Utils.FiniteStateMachine
{
    public interface IStateMachine
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
        public void Update();
    }

    public class SimpleStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();

        private IExitableState _activeState;

        public void AddState<TState>(IExitableState state)
        {
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

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }

    public class Rat : MonoBehaviour
    {
        private void Awake()
        {
            RealTimeStateMachine stateMachine = new RealTimeStateMachine();

            stateMachine
                .AddState<IState>(null).AddTransition<IState, IUpdatableState>(() => true)
                .AddState<IUpdatableState>(null);

        }
    }

    public class StateMachineTransition
    {
        private readonly Func<bool> _condition;

        public bool IsConditionTrue => _condition.Invoke();
        public Type TargetStateType { get; private set; }

        public StateMachineTransition(Type targetStateType, Func<bool> condition)
        {
            _condition = condition;
            TargetStateType = targetStateType;
        }
    }

    public class RealTimeStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private readonly Dictionary<Type, List<StateMachineTransition>> _transitions = new();

        private IExitableState _activeState;

        public RealTimeStateMachine AddState<TState>(IExitableState state)
        {
            _states.Add(typeof(TState), state);

            return this;
        }

        public RealTimeStateMachine AddTransition<TSourceState, TTargetState>(Func<bool> condition)
        {
            if (!_transitions.ContainsKey(typeof(TSourceState)))
                _transitions.Add(typeof(TSourceState), new List<StateMachineTransition>());
                
            _transitions[typeof(TSourceState)].Add(new StateMachineTransition(typeof(TTargetState), condition));

            return this;
        }

        public void Tick()
        {
            foreach (var transition in _transitions[_activeState.GetType()])
            {
                if (transition.IsConditionTrue)
                {
                    _activeState?.Exit();
                    _activeState = _states[transition.TargetStateType];
                    (_activeState as IState)?.Enter();
                }
            }
            
            if (_activeState is IUpdatableState state)
                state.Update();
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

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}