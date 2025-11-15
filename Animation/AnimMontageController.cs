using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Animation
{
    public class AnimMontageController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private AnimMontageInstance currentAnimMontage;
        private AnimationPlayableOutput output;
        
        public void Play(AnimMontage animMontage, UnityAction onComplete)
        {
            if (currentAnimMontage != null)
            {
                currentAnimMontage.Dispose();
            }
            
            //currentAnimMontage = animMontage.Create();
            //currentAnimMontage.OnFinished = onComplete;
            
            //output = AnimationPlayableOutput.Create(currentAnimMontage.Graph, "AnimMontage", animator);
            //currentAnimMontage.Play(ref output);
        }

        public void Stop()
        {
            if (currentAnimMontage != null)
            {
                currentAnimMontage.Dispose();
                currentAnimMontage = null;
            }
        }

        private void LateUpdate()
        {
            if (currentAnimMontage == null)
            {
                return;
            }
            
            currentAnimMontage.Update(Time.deltaTime);

            if (currentAnimMontage.IsFinished)
            {
                currentAnimMontage.OnFinished?.Invoke();
                currentAnimMontage.Dispose();
                currentAnimMontage = null;
            }
        }
    }
}
