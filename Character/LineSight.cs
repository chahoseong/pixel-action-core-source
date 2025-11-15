using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character
{
    [CreateAssetMenu(menuName = "Character/Perception/Sight/Config/Line", fileName = "New Line Sight")]
    public class LineSight : SightConfig
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float range;

        [Header("Debug")]
        [SerializeField] private string debugLabel;

        public override void Accept(Transform transform, LayerMask layerMask, List<GameObject> results)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position + offset,
                transform.right,
                range,
                layerMask
            );

            if (hit)
            {
                results.Add(hit.transform.gameObject);
            }
        }

        public override void DrawDebug(GameObject gameObject)
        {
            Gizmos.color = Color.white;
            Vector3 start = gameObject.transform.position + offset;
            Vector3 end = start + gameObject.transform.right * range;
            Gizmos.DrawLine(start, end);

#if UNITY_EDITOR
            Handles.color = Color.white;
            Handles.Label(end, debugLabel);
#endif
        }
    }
}
