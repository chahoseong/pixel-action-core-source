
using Abilities;
using UnityEngine;

namespace Character.Player.States
{
    public abstract class GroundedState : PlayerState
    {
        protected GroundedState(PlayerCharacter context) : base(context)
        {
        }

        public override void Update(float deltaTime)
        {
            if (Context.JumpAction)
            {
                Context.AbilitySystem.TryExecuteAbility<JumpAbilityDefinition>();
            }

            switch (Context.CharacterMovement.Velocity.y)
            {
                case > 0:
                    Context.StateController.ChangeState<JumpingState>();
                    break;
                case < 0:
                    Context.StateController.ChangeState<FallingState>();
                    break;
                default:
                    base.Update(deltaTime);
                    break;
            }
        }
    }
}
