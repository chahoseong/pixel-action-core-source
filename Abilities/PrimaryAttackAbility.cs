using Animation;
using Character.Player;
using Character.Player.States;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Primary Attack", fileName = "PrimaryAttack Ability")]
    public class PrimaryAttackAbilityDefinition : AbilityDefinition
    {
        [field: SerializeField] public AnimMontage AttackMontage { get; private set; }
        [field: SerializeField] public string AnimationTrigger { get; private set; }
        
        public override AbilityInstance CreateAbility(AbilitySystem abilitySystem)
        {
            return new PrimaryAttackAbilityInstance(abilitySystem, this);
        }
    }

    public class PrimaryAttackAbilityInstance : AbilityInstance
    {
        private PlayerCharacter player;
        
        public PrimaryAttackAbilityInstance(AbilitySystem abilitySystem, AbilityDefinition definition) 
            : base(abilitySystem, definition)
        {
            player = OwningObject.GetComponent<PlayerCharacter>();
        }

        public override AbilityInstance Clone()
        {
            return new PrimaryAttackAbilityInstance(OwningAbilitySystem, Definition);
        }

        public override void OnStart()
        {
            base.OnStart();

            Debug.Log("Primary Attack Start");
            
            var def = GetDefinition<PrimaryAttackAbilityDefinition>();
            PlayAnimMontage(def.AttackMontage, OnComplete);
            player.StateController.ChangeState<AttackState>();
        }

        private void OnComplete()
        {
            EndAbility();
            Debug.Log("Primary Attack End");
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            if (player.AttackAction)
            {
                // Next Attack
            }
        }

        public override void OnEnd()
        {
            player.StateController.ChangeState<StandingState>();
            base.OnEnd();
        }
    }
}
