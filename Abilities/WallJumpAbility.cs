using Character;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Wall Jump", fileName = "WallJump Ability")]
    public class WallJumpAbilityDefinition : AbilityDefinition
    {
        [field: SerializeField] public Vector2 JumpForce { get; private set; }
        
        public override AbilityInstance CreateAbility(AbilitySystem abilitySystem)
        {
            return new WallJumpAbilityInstance(abilitySystem, this);
        }
    }

    public class WallJumpAbilityInstance : AbilityInstance
    {
        private CharacterMovement characterMovement;
        
        public WallJumpAbilityInstance(AbilitySystem abilitySystem, AbilityDefinition definition) 
            : base(abilitySystem, definition)
        {
            characterMovement = OwningObject.GetComponent<CharacterMovement>();
        }

        public override AbilityInstance Clone()
        {
            return new WallJumpAbilityInstance(OwningAbilitySystem, GetDefinition<WallJumpAbilityDefinition>());
        }

        public override void OnStart()
        {
            base.OnStart();
            
            var def = GetDefinition<WallJumpAbilityDefinition>();
            
            characterMovement.Velocity = new Vector2(
                def.JumpForce.x * characterMovement.FacingDirection,
                def.JumpForce.y
            );
            
            EndAbility();
        }
    }
}
