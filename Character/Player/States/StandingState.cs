
namespace Character.Player.States
{
    public class StandingState : GroundedState
    {
        public StandingState(PlayerCharacter context) : base(context)
        {
        }

        public override void Update(float deltaTime)
        {
            if (Context.MoveAction.x != 0.0f)
            {
                Context.StateController.ChangeState<MovingState>();
            }
            else
            {
                base.Update(deltaTime);
            }
        }
    }
}
