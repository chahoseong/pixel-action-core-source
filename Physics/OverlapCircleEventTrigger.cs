using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OverlapCircleEventTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private LayerMask layerMask;
    
    [Header("Debug")]
    [SerializeField] private bool drawDebug = false;
    [SerializeField] private string label;

    private Collider2D hit;

    public Collider2D HitResult => hit;
    public bool IsDetected => hit;
    
    private void FixedUpdate()
    {
        hit = Physics2D.OverlapCircle(
            transform.position + offset,
            radius,
            layerMask
        );
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + offset, radius);

            Handles.color = Color.white;
            Handles.Label(transform.position + offset, label);
        }
    }
#endif
}
