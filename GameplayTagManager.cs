using System.Collections.Generic;
using Tags;
using UnityEngine;

[CreateAssetMenu(menuName = "GameplayTags/Manager", fileName = "New GameplayTagsManager")]
public class GameplayTagManager : ScriptableObject
{
    [field: SerializeField] public GameplayTag Input_Action_Jump { get; private set; }
    [field: SerializeField] public GameplayTag State_Grabbing { get; private set; }
}
