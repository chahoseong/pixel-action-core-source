using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateController
    {
        private Dictionary<System.Type, IState> states = new();
        private IState currentState;
        private bool hasTransition;
        
        public IState CurrentState => currentState;

        public void Update(float deltaTime)
        {
            hasTransition = false;
            currentState?.Update(deltaTime);
        }

        public void AddState<T>(T state) where T : IState
        {
            states.Add(typeof(T), state);
        }

        public T GetState<T>() where T : IState
        {
            states.TryGetValue(typeof(T), out var state);
            return (T)state;
        }

        public void ChangeState<T>() where T : IState
        {
            if (hasTransition)
            {
                return;
            }
            currentState?.Exit();
            states.TryGetValue(typeof(T), out var nextState);
            nextState?.Enter();
            currentState = nextState;
            hasTransition = currentState != null;
        }
    }
}
