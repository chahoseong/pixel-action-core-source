using System.Collections.Generic;
using Animation;
using Tags;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public abstract class AbilityInstance
    {
        private AnimMontageController animMontageController;
        private Dictionary<GameplayTag, UnityAction> eventCallbacks = new();
        
        public AbilityDefinition Definition { get; }
        public List<GameplayTag> RuntimeGameplayTags { get; }

        public bool IsRunning { get; private set; }

        public GameObject OwningObject => OwningAbilitySystem.gameObject;
        public AbilitySystem OwningAbilitySystem { get; }
        
        protected AbilityInstance(AbilitySystem abilitySystem, AbilityDefinition definition)
        {
            OwningAbilitySystem = abilitySystem;
            Definition = definition;
            RuntimeGameplayTags = new List<GameplayTag>();
            animMontageController = OwningObject.GetComponentInChildren<AnimMontageController>();
        }

        public abstract AbilityInstance Clone();

        public virtual bool CanExecute()
        {
            return true;
        }
        
        public virtual void OnStart()
        {
            IsRunning = true;
        }

        public virtual void OnUpdate(float deltaTime)
        {
            
        }

        public virtual void OnEnd()
        {
            foreach (var pair in eventCallbacks)
            {
                OwningAbilitySystem.UnregisterGameplayEvent(pair.Key, pair.Value);
            }
            
            IsRunning = false;
        }

        protected void EndAbility()
        {
            OwningAbilitySystem.EndAbility(this);
        }

        protected void SendGameplayEvent(GameplayTag eventTag)
        {
            OwningAbilitySystem.SendGameplayEvent(eventTag);
        }

        protected void WaitGameplayEvent(GameplayTag eventTag, UnityAction callback)
        {
            if (eventCallbacks.TryGetValue(eventTag, out var existingCallbacks))
            {
                existingCallbacks += callback;
            }
            else
            {
                eventCallbacks.Add(eventTag, callback);
            }
            OwningAbilitySystem.RegisterGameplayEvent(eventTag, callback);
        }

        protected void PlayAnimMontage(AnimMontage animMontage, UnityAction onComplete)
        {
            animMontageController.Play(animMontage, onComplete);
        }

        public T GetDefinition<T>() where T : AbilityDefinition
        {
            return (T)Definition;
        }
    }
}
