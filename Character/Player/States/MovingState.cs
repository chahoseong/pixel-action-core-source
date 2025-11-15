using UnityEngine;

namespace Character.Player.States
{
    public class MovingState : GroundedState
    {
        private float stopTime = 0.0f;
        
        public MovingState(PlayerCharacter context) : base(context)
        {

        }

        public override void Enter()
        {
            base.Enter();
            Context.PlayerAnimator.SetIsMoving(true);
        }

        public override void Update(float deltaTime)
        {
            if (Context.MoveAction.x == 0.0f)
            {
                stopTime += deltaTime;

                if (stopTime > 0.1f)
                {
                    Context.StateController.ChangeState<StandingState>();
                }
                else
                {
                    base.Update(deltaTime);
                }
            }
            else
            {
                Vector2 direction = new Vector2(Context.MoveAction.x, 0.0f);
                Context.CharacterMovement.Move(direction);

                stopTime = 0.0f;
                
                if (ShouldPivot())
                {
                    Context.StateController.ChangeState<PivotState>();
                }
                else
                {
                    base.Update(deltaTime);
                }
            }
        }

        private bool ShouldPivot()
        {
            return Context.MoveAction.x > 0 && Context.CharacterMovement.Velocity.x < 0 ||
                   Context.MoveAction.x < 0 && Context.CharacterMovement.Velocity.x > 0;
        }
        
        public override void Exit()
        {
            Context.PlayerAnimator.SetIsMoving(false);
            base.Exit();
        }
    }
}
