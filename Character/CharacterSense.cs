using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public abstract class CharacterSense : ScriptableObject
    {
        [SerializeField] private string key;

        public string Key => key;

        public abstract void Perceive(CharacterPerception context, List<GameObject> results);

        public virtual void DrawDebug(GameObject gameObject)
        {
        }
    }
}
