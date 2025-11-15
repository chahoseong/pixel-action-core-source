using Tags;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Ability Set", fileName = "New AbilitySet")]
    public class AbilitySet : ScriptableObject
    {
        [System.Serializable]
        public struct AbilityEntry
        {
            public AbilityDefinition ability;
            public GameplayTag inputTag;
        }
        
        [SerializeField] private AbilityEntry[] abilities;

        public void GiveAbilities(AbilitySystem abilitySystem)
        {
            foreach (var abilityToGrant in abilities)
            {
                var abilityInstance = abilityToGrant.ability.CreateAbility(abilitySystem);
                if (abilityToGrant.inputTag)
                {
                    abilityInstance.RuntimeGameplayTags.Add(abilityToGrant.inputTag);
                }
                abilitySystem.GiveAbility(abilityInstance);
            }
        }

        public AbilityDefinition FindAbilityWithTag(GameplayTag inputTag)
        {
            foreach (var abilityToGrant in abilities)
            {
                if (abilityToGrant.inputTag == inputTag)
                {
                    return abilityToGrant.ability;
                }
            }

            return null;
        }
    }
}
