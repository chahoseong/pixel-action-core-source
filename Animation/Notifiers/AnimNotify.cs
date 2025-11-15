using UnityEngine;

namespace Animation.Notifiers
{
    public abstract class AnimNotify : ScriptableObject
    {
        public virtual void OnNotify(Animator animator)
        {
        }
    }
}
