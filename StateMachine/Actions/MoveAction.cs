using Characters;
using UnityEngine;

namespace StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Move", fileName = "MoveAction")]
    public class MoveAction : Action
    {
        public override void Execute(object context)
        {
            if (context is PlayerCharacter playerCharacter)
            {
                var velocity = new Vector2(playerCharacter.MoveInput.x * playerCharacter.Speed, 0.0f);
                playerCharacter.Rigidbody.linearVelocity = velocity;

                var shouldFlip = (playerCharacter.FacingDirection > 0 && playerCharacter.MoveInput.x < 0)
                                 || (playerCharacter.FacingDirection < 0 && playerCharacter.MoveInput.x > 0);
                if (shouldFlip)
                {
                    playerCharacter.Flip();
                }
            }
        }
    }
}
