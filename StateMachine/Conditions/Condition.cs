using UnityEngine;

namespace StateMachine.Conditions
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool Evaluate(object context);
    }
}
