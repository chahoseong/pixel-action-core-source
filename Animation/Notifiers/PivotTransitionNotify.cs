using Character.Player;
using UnityEngine;

namespace Animation.Notifiers
{
    [CreateAssetMenu(menuName = "Animation/Notifiers/Pivot Transition", fileName = "AnimNotify_PivotTransition")]
    public class PivotTransitionNotify : AnimNotify
    {
        public override void OnNotify(Animator animator)
        {
            PlayerCharacter player = animator.GetComponentInParent<PlayerCharacter>();
            player.PlayerAnimator.PivotCompleted = true;
        }
    }
}
