using UnityEngine;

[CreateAssetMenu(menuName = "AnimNotify", fileName = "New AnimNotify")]
public class AnimNotify : ScriptableObject
{
    public virtual void OnNotify(Animator animator)
    {
        
    }
}
