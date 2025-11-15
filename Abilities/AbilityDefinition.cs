using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Abilities
{
    public enum AbilityInstancingPolicy
    {
        InstancedPerGameObject,
        InstancedPerExecution,
    }
    
    public abstract class AbilityDefinition : ScriptableObject
    {
        [SerializeField] private List<GameplayTag> abilityTags;
        [SerializeField] private List<GameplayTag> requiredTags;
        [SerializeField] private AbilityInstancingPolicy instancingPolicy;
        
        public IEnumerable<GameplayTag> AbilityTags => abilityTags;
        public IEnumerable<GameplayTag> RequiredTags => requiredTags;
        public AbilityInstancingPolicy InstancingPolicy => instancingPolicy;
        
        public abstract AbilityInstance CreateAbility(AbilitySystem abilitySystem);
    }
}
