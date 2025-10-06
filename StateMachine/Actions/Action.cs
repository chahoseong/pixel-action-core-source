using UnityEngine;

namespace StateMachine.Actions
{
    public abstract class Action : ScriptableObject
    {
        public virtual void Ready(object context)
        {

        }

        public virtual void Finish(object context)
        {

        }

        public virtual void Execute(object context)
        {

        }
    }
}
