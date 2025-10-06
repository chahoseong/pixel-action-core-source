using Characters;
using UnityEngine;

namespace StateMachine.Conditions
{
    [CreateAssetMenu(menuName = "State Machine/Conditions/Has Move Input", fileName = "HasMoveInput")]
    public class HasMoveInput : Condition
    {
        [SerializeField] private bool invert = false;

        public override bool Evaluate(object context)
        {
            if (context is PlayerCharacter playerCharacter)
            {
                var result = playerCharacter.MoveInput.x != 0;
                result = invert ? !result : result;
                return result;
            }

            return false;
        }
    }
}
