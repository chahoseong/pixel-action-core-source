using UnityEngine;

namespace Character.Player.States
{
    public class FallingState : AirborneState
    {
        public FallingState(PlayerCharacter context) : base(context)
        {
        }

        public override void Update(float deltaTime)
        {
            if (IsWallDetected() && IsMovingTowardsWall())
            {
                Context.StateController.ChangeState<WallSlidingState>();
            }
            else
            {
                base.Update(deltaTime);
            }
        }

        private bool IsWallDetected()
        {
            return Context.CharacterPerception.IsPerceived("Wall");
        }

        private bool IsMovingTowardsWall()
        {
            var result = Context.CharacterPerception.GetPerceptionResults("Wall");
            Vector3 toWall = result.objects[0].transform.position - Context.transform.position;
            return (toWall.x > 0 && Context.MoveAction.x > 0) || 
                   (toWall.x < 0 && Context.MoveAction.x < 0);
        }
    }
}
