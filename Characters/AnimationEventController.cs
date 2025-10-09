using UnityEngine;
using UnityEngine.Events;

public class AnimationEventController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OnAnimNotify(AnimNotify animNotify)
    {
        animNotify.OnNotify(animator);
    }
}
