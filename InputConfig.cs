using System.Collections.Generic;
using System.Linq;
using Tags;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/Config", fileName = "New Input Config")]
public class InputConfig : ScriptableObject
{
    [System.Serializable]
    public struct InputAction
    {
        public string actionName;
        public GameplayTag inputTag;
    }
    
    [SerializeField] private List<InputAction> inputActions;

    private Dictionary<string, GameplayTag> actionsToTags = new();

    public void Initialize()
    {
        actionsToTags = inputActions.ToDictionary(
            x => x.actionName,
            y => y.inputTag
        );
    }

    public GameplayTag FindInputTag(string actionName)
    {
        return actionsToTags.GetValueOrDefault(actionName);
    }
}
