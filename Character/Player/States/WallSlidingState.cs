using Abilities;
using UnityEngine;

namespace Character.Player.States
{
    public class WallSlidingState : PlayerState
    {
        public WallSlidingState(PlayerCharacter context) : base(context)
        {

        }

        public override void Enter()
        {
            base.Enter();
            Context.PlayerAnimator.SetIsWallSliding(true);
        }

        public override void Update(float deltaTime)
        {
            if (Context.CharacterMovement.IsGrounded)
            {
                Context.StateController.ChangeState<StandingState>();
                return;
            }

            if (IsWallSliding())
            {
                Context.CharacterMovement.Velocity = new Vector2(
                    Context.CharacterMovement.Velocity.x,
                    Context.WallSlidingSpeed
                );
                
                if (Context.JumpAction)
                {
                    Context.StateController.ChangeState<WallJumpingState>();
                }
                else
                {
                    base.Update(deltaTime);
                }
                
            }
            else switch (Context.CharacterMovement.Velocity.y)
            {
                case > 0:
                    Context.StateController.ChangeState<JumpingState>();
                    break;
                case < 0:
                    Context.StateController.ChangeState<FallingState>();
                    break;
            }
        }

        private bool IsWallSliding()
        {
            PerceptionResult result = Context.CharacterPerception.GetPerceptionResults("Wall");
            
            if (result.objects.Count <= 0)
            {
                return false;
            }
            
            Vector3 toWall = result.objects[0].transform.position - Context.transform.position;
            return toWall.x > 0 && Context.MoveAction.x > 0 ||
                   toWall.x < 0 && Context.MoveAction.x < 0;
        }

        public override void Exit()
        {
            Context.PlayerAnimator.SetIsWallSliding(false);
            base.Exit();
        }
    }
}
