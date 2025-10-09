using Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "AnimNotify/Pivot", fileName = "AN_Pivot")]
public class AnimNotify_Pivot : AnimNotify
{
    public override void OnNotify(Animator animator)
    {
        Character character = animator.gameObject.GetComponentInParent<Character>();
        character.Flip();
    }
}
