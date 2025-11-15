using UnityEngine;

namespace Character.Player.States
{
    public class PivotState : GroundedState
    {
        public PivotState(PlayerCharacter context) : base(context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Context.PlayerAnimator.PivotCompleted = false;
            Context.PlayerAnimator.SetIsPivoting(true);
        }

        public override void Update(float deltaTime)
        {
            if (Context.PlayerAnimator.PivotCompleted)
            {
                Context.CharacterMovement.Flip();
                Context.StateController.ChangeState<MovingState>();
            }
            else
            {
                base.Update(deltaTime);
            }
        }

        public override void Exit()
        {
            Context.PlayerAnimator.SetIsPivoting(false);
            base.Exit();
        }
    }
}
