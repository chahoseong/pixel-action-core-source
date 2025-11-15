using Character;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Jump", fileName = "Jump Ability")]
    public class JumpAbilityDefinition : AbilityDefinition
    {
        [SerializeField] private float jumpForce;
        
        public float JumpForce => jumpForce;

        public override AbilityInstance CreateAbility(AbilitySystem abilitySystem)
        {
            return new JumpAbilityInstance(abilitySystem, this);
        }
    }

    public class JumpAbilityInstance : AbilityInstance
    {
        private CharacterMovement characterMovement;
        
        public JumpAbilityInstance(AbilitySystem abilitySystem, JumpAbilityDefinition definition) 
            : base(abilitySystem, definition)
        {
            characterMovement = OwningObject.GetComponent<CharacterMovement>();
        }

        public override AbilityInstance Clone()
        {
            return new JumpAbilityInstance(OwningAbilitySystem, GetDefinition<JumpAbilityDefinition>());
        }

        public override bool CanExecute()
        {
            return characterMovement.IsGrounded;
        }

        public override void OnStart()
        {
            characterMovement.Velocity = new Vector2(
                characterMovement.Velocity.x,
                GetDefinition<JumpAbilityDefinition>().JumpForce
            );
            
            EndAbility();
        }
    }
}
