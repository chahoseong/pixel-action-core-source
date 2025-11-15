using System.Linq;
using UnityEngine;

namespace Tags
{
    [CreateAssetMenu(menuName="Tags/Gameplay Tag", fileName = "New GameplayTag")]
    public class GameplayTag : ScriptableObject
    {
        [SerializeField] private string tagName;
        
        public override bool Equals(object other)
        {
            if (other is GameplayTag gameplayTag)
            {
                return Equals(gameplayTag);
            }
            return false;
        }

        private bool Equals(GameplayTag other)
        {
            string[] myTags = tagName.Split('.');
            string[] otherTags = other.tagName.Split('.');

            if (myTags.Length != otherTags.Length)
            {
                return false;
            }

            return !myTags.Where((tag, index) => tag != otherTags[index])
                .Any();
        }

        public override int GetHashCode()
        {
            return tagName.GetHashCode();
        }

        public override string ToString()
        {
            return tagName;
        }
    }
}
