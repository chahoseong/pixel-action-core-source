using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Character/Perception/Sight", fileName = "New Sight")]
    public class CharacterSight : CharacterSense
    {
        [SerializeField] private SightConfig config;
        [SerializeField] private LayerMask layerMask;

        public override void Perceive(CharacterPerception context, List<GameObject> results)
        {
            config.Accept(context.transform, layerMask, results);
        }

        public override void DrawDebug(GameObject gameObject)
        {
            config.DrawDebug(gameObject);
        }
    }
}
