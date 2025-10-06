using StateMachine.Conditions;

namespace StateMachine
{
    [System.Serializable]
    public struct Transition
    {
        public Condition Condition;
        public State NextState;

        public bool IsValid => Condition != null && NextState != null;
    }
}
