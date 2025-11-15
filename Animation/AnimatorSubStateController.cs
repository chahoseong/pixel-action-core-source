using UnityEngine;

namespace Animation
{
    public class AnimatorSubStateController : StateMachineBehaviour
    {
        [SerializeField] private int number;
        
        private readonly int subState = Animator.StringToHash("SubState");
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger(subState, number);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger(subState, -1);
        }
    }
}
