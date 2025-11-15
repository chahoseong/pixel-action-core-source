using Abilities;
using UnityEngine;

namespace Character.Player.States
{
    public class WallJumpingState : PlayerState
    {
        private bool jumpStarted;
        private int desiredJumpDirection;
        
        public WallJumpingState(PlayerCharacter context) : base(context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            jumpStarted = false;
            desiredJumpDirection = -Context.CharacterMovement.FacingDirection;
            
            Context.PlayerAnimator.SetIsWallJumping(true);
        }
        
        public override void Update(float deltaTime)
        {
            if (Context.PlayerAnimator.WallJumpStart)
            {
                Context.AbilitySystem.TryExecuteAbility<WallJumpAbilityDefinition>();
                Context.PlayerAnimator.WallJumpStart = false;
                jumpStarted = true;
            }

            if (jumpStarted)
            {
                if (Context.CharacterMovement.Velocity.y < 0)
                {
                    Context.StateController.ChangeState<FallingState>();
                    return;
                }
            }
            else
            {
                Context.CharacterMovement.Velocity = new Vector2(
                    Context.CharacterMovement.Velocity.x,
                    0.0f
                );
                Context.CharacterMovement.SetFacingDirection(desiredJumpDirection);
            }
            
            base.Update(deltaTime);
        }

        public override void Exit()
        {
            Context.PlayerAnimator.SetIsWallJumping(false);
            base.Exit();
        }
    }
}
