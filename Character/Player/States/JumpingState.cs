
namespace Character.Player.States
{
    public class JumpingState : AirborneState
    {
        public JumpingState(PlayerCharacter context) : base(context)
        {
        }

        public override void Update(float deltaTime)
        {
            if (IsFalling())
            {
                Context.StateController.ChangeState<FallingState>();
            }
            else
            {
                base.Update(deltaTime);
            }
        }

        private bool IsFalling()
        {
            return Context.CharacterMovement.Velocity.y < 0.0f &&
                   !Context.CharacterMovement.IsGrounded;
        }
    }
}
