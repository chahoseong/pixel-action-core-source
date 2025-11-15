using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public readonly struct PerceptionResult
    {
        public readonly List<GameObject> objects;

        public PerceptionResult(List<GameObject> objects)
        {
            this.objects = objects;
        }
    }

    public class CharacterPerception : MonoBehaviour, ScriptUpdateScheduler.IUpdatable
    {
        private struct SenseResult
        {
            public List<GameObject> objects;
        }

        [SerializeField] private List<CharacterSense> senses;

        [Header("Debug")]
        [SerializeField] private bool drawDebug = false;

        private readonly Dictionary<string, SenseResult> senseResults = new();

        public int UpdateOrder => 50;

        private void Awake()
        {
            foreach (var sense in senses)
            {
                var result = new SenseResult
                {
                    objects = new List<GameObject>()
                };
                senseResults.Add(sense.Key, result);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var sense in senses)
            {
                var result = senseResults[sense.Key];
                result.objects.Clear();
                sense.Perceive(this, result.objects);
            }
        }

        public PerceptionResult GetPerceptionResults(string key)
        {
            senseResults.TryGetValue(key, out var result);
            return new PerceptionResult(result.objects);
        }

        public bool IsPerceived(string key)
        {
            senseResults.TryGetValue(key, out var result);
            return result.objects.Count > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (drawDebug)
            {
                foreach (var sense in senses)
                {
                    sense.DrawDebug(gameObject);
                }
            }
        }
#endif
    }
}
