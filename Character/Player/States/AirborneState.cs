using UnityEngine;

namespace Character.Player.States
{
    public abstract class AirborneState : PlayerState
    {
        protected AirborneState(PlayerCharacter context) : base(context)
        {

        }

        public override void Update(float deltaTime)
        {
            Vector2 movement = new Vector2(Context.MoveAction.x, 0.0f);
            Context.CharacterMovement.Move(movement);

            if (!IsLanded())
            {
                base.Update(deltaTime);
                return;
            }

            if (Context.CharacterMovement.Velocity.x != 0.0f)
            {
                Context.StateController.ChangeState<MovingState>();
            }
            else
            {
                Context.StateController.ChangeState<StandingState>();
            }
        }

        private bool IsLanded()
        {
            return Context.CharacterMovement.Velocity.y <= 0.0f &&
                   Context.CharacterMovement.IsGrounded;
        }
    }
}
