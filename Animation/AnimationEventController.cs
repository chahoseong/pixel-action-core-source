using UnityEngine;
using Animation.Notifiers;

namespace Animation
{
    public class AnimationEventController : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    
        public void OnAnimNotify(AnimNotify animationEvent)
        {
            animationEvent.OnNotify(animator);
        }
    }
}
