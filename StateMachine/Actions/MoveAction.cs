using Characters;
using UnityEngine;

namespace StateMachine.Actions
{
    [CreateAssetMenu(menuName = "State Machine/Actions/Move", fileName = "MoveAction")]
    public class MoveAction : Action
    {
        public override void Ready(object context)
        {
            if (context is Character character)
            {
                if (character.ShouldFlip())
                {
                    character.Flip();
                }
            }
        }

        public override void Execute(object context)
        {
            if (context is Character character)
            {
                Vector2 currentVelocity = character.Velocity;
                currentVelocity.x += character.Acceleration.x * Time.deltaTime;
                currentVelocity.x = Mathf.Clamp(
                    currentVelocity.x,
                    -character.MaxMoveSpeed,
                    character.MaxMoveSpeed
                );
                character.Velocity = currentVelocity;
            }
        }
    }
}
