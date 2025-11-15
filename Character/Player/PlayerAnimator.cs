using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    public class PlayerAnimator
    {
        private readonly Animator animator;
        private readonly Dictionary<string, int> stringToHash = new();
        
        private readonly int velocityX = Animator.StringToHash("VelocityX");
        private readonly int velocityY = Animator.StringToHash("VelocityY");
        private readonly int velocityMagnitude = Animator.StringToHash("VelocityMagnitude");
        private readonly int hasVelocity = Animator.StringToHash("HasVelocity");
        
        private readonly int isGrounded = Animator.StringToHash("IsGrounded");
        private readonly int isMoving = Animator.StringToHash("IsMoving");
        private readonly int isPivoting = Animator.StringToHash("IsPivoting");
        private readonly int isWallSliding = Animator.StringToHash("IsWallSliding");
        private readonly int isWallJumping = Animator.StringToHash("IsWallJumping");
        
        public bool PivotCompleted { get; set; }
        public bool WallJumpStart { get; set; }
        
        public PlayerAnimator(Animator animator)
        {
            this.animator = animator;
        }

        public void SetVelocity(Vector2 value)
        {
            animator.SetFloat(velocityX, value.x);
            animator.SetFloat(velocityY, value.y);
            animator.SetFloat(velocityMagnitude, value.magnitude);
            animator.SetBool(hasVelocity, value.x != 0.0f || value.y != 0.0f);
        }

        public void SetIsMoving(bool value)
        {
            animator.SetBool(isMoving, value);
        }

        public void SetIsGrounded(bool value)
        {
            animator.SetBool(isGrounded, value);
        }

        public void SetIsPivoting(bool value)
        {
            animator.SetBool(isPivoting, value);
        }
        
        public void SetIsWallSliding(bool value)
        {
            animator.SetBool(isWallSliding, value);
        }

        public void SetIsWallJumping(bool value)
        {
            animator.SetBool(isWallJumping, value);
        }

        public void SetTrigger(string triggerName)
        {
            if (!stringToHash.TryGetValue(triggerName, out var hash))
            {
                hash = Animator.StringToHash(triggerName);
                stringToHash.Add(triggerName, hash);
            }
            animator.SetTrigger(hash);
        }

        public void ResetTrigger(string triggerName)
        {
            if (!stringToHash.TryGetValue(triggerName, out var hash))
            {
                hash = Animator.StringToHash(triggerName);
                stringToHash.Add(triggerName, hash);
            }
            animator.ResetTrigger(hash);
        }
    }
}
