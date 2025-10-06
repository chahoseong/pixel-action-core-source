using UnityEngine;

namespace StateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private State initState;
        [SerializeField, ReadOnly] private State currentState;

        public object Context { get; set; }

        private void Start()
        {
            ChangeState(initState);
        }

        private void LateUpdate()
        {
            currentState?.Tick(this);
        }

        public void ChangeState(State nextState)
        {
            currentState?.Exit(this);
            nextState?.Enter(this);
            currentState = nextState;
        }
    }
}
