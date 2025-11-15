using Character.Player;
using UnityEngine;

namespace Animation.Notifiers
{
    [CreateAssetMenu(menuName = "Animation/Notifiers/Wall Jump Start", fileName = "AnimNotify_WallJumpStart")]
    public class WallJumpStartNotify : AnimNotify
    {
        public override void OnNotify(Animator animator)
        {
            var player = animator.GetComponentInParent<PlayerCharacter>();
            player.PlayerAnimator.WallJumpStart = true;
        }
    }
}
