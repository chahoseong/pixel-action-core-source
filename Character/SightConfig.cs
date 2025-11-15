using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public abstract class SightConfig : ScriptableObject
    {
        public abstract void Accept(Transform transform, LayerMask layerMask, List<GameObject> results);

        public virtual void DrawDebug(GameObject gameObject)
        {
        }
    }
}
