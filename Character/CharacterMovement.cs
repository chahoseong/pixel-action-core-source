using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Character
{
    public class CharacterMovement : MonoBehaviour, ScriptUpdateScheduler.IFixedUpdatable,
        ScriptUpdateScheduler.IUpdatable
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D physicsBody;
        [SerializeField] private int fixedUpdateOrder = 100;
        [SerializeField] private int updateOrder = 150;

        [Header("Movement")]
        [SerializeField] private float maxMoveSpeed = 10.0f;
        [SerializeField] private float maxAcceleration = 10.0f;
        [SerializeField] private float brakingDeceleration = 10.0f;

        [Header("Ground Detection")]
        [SerializeField] private Vector3 groundDetectionOffset;
        [SerializeField] private float groundDetectionRange;
        [SerializeField] private LayerMask groundLayers;

        [Header("Debug")]
        [SerializeField] private bool drawDebug = false;
        [SerializeField, ReadOnly] private Vector2 debugVelocity;
        
        private int facingDirection = 1;
        private Vector2 moveDirection = Vector2.zero;
        private Vector2 acceleration = Vector2.zero;
        private bool isGrounded = false;

        public int FixedUpdateOrder => fixedUpdateOrder;
        public int UpdateOrder => updateOrder;

        public Vector2 Velocity
        {
            get => physicsBody.linearVelocity;
            set => physicsBody.linearVelocity = value;
        }

        public Vector2 Acceleration => acceleration;

        public int FacingDirection => facingDirection;
        public bool IsGrounded => isGrounded;
        public bool IsFalling => !isGrounded;

        public float MaxMoveSpeed => maxMoveSpeed;
        public float MaxAcceleration => maxAcceleration;
        public float BrakingDeceleration => brakingDeceleration;

        
        private void Start()
        {
            facingDirection = transform.right.x >= 0 ? 1 : -1;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            isGrounded = Physics2D.OverlapCircle(
                transform.position + groundDetectionOffset,
                groundDetectionRange,
                groundLayers
            );
        }

        public void OnUpdate(float deltaTime)
        {
            UpdateVelocity(deltaTime);
            if (ShouldFlip())
            {
                Flip();
            }
        }

        private void UpdateVelocity(float deltaTime)
        {
            Vector2 desiredVelocity = new Vector2(
                moveDirection.x * maxMoveSpeed,
                Velocity.y
            );
            float step = HasMovement() ? maxAcceleration : brakingDeceleration;
            Velocity = Vector2.MoveTowards(
                Velocity,
                desiredVelocity,
                step * deltaTime
            );

            debugVelocity = Velocity;
            
            acceleration = moveDirection * maxAcceleration;
            moveDirection = Vector2.zero;
        }

        private bool HasMovement()
        {
            return moveDirection.x != 0.0f || moveDirection.y != 0.0f;
        }

        private bool ShouldFlip()
        {
            return facingDirection < 0 && Velocity.x > 0 ||
                   facingDirection > 0 && Velocity.x < 0;
        }

        public void Flip()
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            facingDirection *= -1;
        }

        public void SetFacingDirection(int direction)
        {
            if (direction > 0)
            {
                transform.rotation = Quaternion.identity;
                facingDirection = 1;
            }
            else if (direction < 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                facingDirection = -1;
            }
        }

        public void Move(Vector2 direction)
        {
            moveDirection = direction;
            acceleration = moveDirection * maxAcceleration;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (drawDebug)
            {
                DrawGroundDetection();
                DrawFacingDirection();
            }
        }

        private void DrawGroundDetection()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + groundDetectionOffset, groundDetectionRange);
            Handles.color = Color.white;
            Handles.Label(transform.position + groundDetectionOffset, "Ground");
        }

        private void DrawFacingDirection()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * facingDirection);
            Handles.color = Color.white;
            Handles.Label(transform.position + Vector3.right * facingDirection, "Facing Direction");
        }
#endif
    }
}
