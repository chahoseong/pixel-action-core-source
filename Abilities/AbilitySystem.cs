using System.Collections.Generic;
using System.Linq;
using Tags;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilitySystem : MonoBehaviour
    {
        private List<AbilityInstance> executableAbilities = new();
        private List<AbilityInstance> runningAbilities = new();
        private List<AbilityInstance> finishedAbilities = new();
        private Dictionary<GameplayTag, int> runtimeGameplayTags = new();
        private Dictionary<GameplayTag, UnityAction> gameplayEventListeners = new();

        public IEnumerable<AbilityInstance> ExecutableAbilities => executableAbilities;

        private void Update()
        {
            foreach (var ability in runningAbilities)
            {
                ability.OnUpdate(Time.deltaTime);
            }

            foreach (var ability in finishedAbilities)
            {
                runningAbilities.Remove(ability);
            }
            finishedAbilities.Clear();
        }
        
        public void GiveAbility(AbilityInstance ability)
        {
            executableAbilities.Add(ability);
        }
        
        public void GiveAbility(AbilityDefinition definition)
        {
            var instance = definition.CreateAbility(this);
            executableAbilities.Add(instance);
        }

        public bool TryExecuteAbility<T>() where T : AbilityDefinition
        {
            var ability = executableAbilities.FirstOrDefault(x => x.Definition is T);
            return TryExecuteAbility(ability);
        }

        public bool TryExecuteAbility(AbilityDefinition definition)
        {
            var ability = executableAbilities.FirstOrDefault(x => x.Definition == definition);
            return TryExecuteAbility(ability);
        }
        
        private bool TryExecuteAbility(AbilityInstance ability)
        {
            if (ability == null)
            {
                return false;
            }
            
            foreach (var gameplayTag in ability.Definition.RequiredTags)
            {
                if (!runtimeGameplayTags.Keys.Contains(gameplayTag))
                {
                    return false;
                }
            }
            
            if (ability.Definition.InstancingPolicy == AbilityInstancingPolicy.InstancedPerGameObject)
            {
                if (ability.IsRunning)
                {
                    return false;
                }

                if (!ability.CanExecute())
                {
                    return false;
                }

                ability.OnStart();
                
                runningAbilities.Add(ability);
            }
            else
            {
                AbilityInstance newInstance = ability.Clone();
                newInstance.OnStart();
                runningAbilities.Add(newInstance);
            }
            
            return true;
        }

        public void EndAbility(AbilityInstance ability)
        {
            ability.OnEnd();
            finishedAbilities.Add(ability);
        }

        public void AddGameplayTag(GameplayTag gameplayTag)
        {
            runtimeGameplayTags[gameplayTag] += 1;
        }

        public void RemoveGameplayTag(GameplayTag gameplayTag)
        {
            runtimeGameplayTags.TryGetValue(gameplayTag, out var count);
            count -= 1;
            if (count <= 0)
            {
                runtimeGameplayTags.Remove(gameplayTag);
            }
        }

        public void SetGameplayTag(GameplayTag gameplayTag, int count)
        {
            runtimeGameplayTags[gameplayTag] = count;
        }

        public void RegisterGameplayEvent(GameplayTag eventTag, UnityAction callback)
        {
            if (gameplayEventListeners.TryGetValue(eventTag, out var existingCallback))
            {
                existingCallback += callback;
            }
            else
            {
                gameplayEventListeners.Add(eventTag, callback);
            }
        }

        public void UnregisterGameplayEvent(GameplayTag eventTag, UnityAction callback)
        {
            if (gameplayEventListeners.TryGetValue(eventTag, out var existingCallbacks))
            {
                existingCallbacks -= callback;
                if (existingCallbacks.GetInvocationList().Length == 0)
                {
                    gameplayEventListeners.Remove(eventTag);
                }
            }
        }

        public void SendGameplayEvent(GameplayTag eventTag)
        {
            if (gameplayEventListeners.TryGetValue(eventTag, out var callbacks))
            {
                callbacks?.Invoke();
            }
        }
    }
}
