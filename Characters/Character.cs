using StateMachine;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D physicsBody;
        [SerializeField] private StateController stateController;

        [SerializeField] private float maxMoveSpeed = 10.0f;
        [SerializeField] private float maxAcceleration = 10.0f;

        private int facingDirection = 1;

        public Animator Animator => animator;
        public Rigidbody2D Rigidbody => physicsBody;
        public float MaxMoveSpeed => maxMoveSpeed;
        public float MaxAcceleration => maxAcceleration;
        public Vector2 Velocity
        {
            get => physicsBody.linearVelocity;
            set => physicsBody.linearVelocity = value;
        }
        public Vector2 Acceleration { get; set; }
        public int FacingDirection => facingDirection;

        protected virtual void Awake()
        {
            stateController.Context = this;
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {
            animator.SetFloat("VelocityX", Velocity.x);
            animator.SetFloat("AccelerationX", Acceleration.x);
            animator.SetFloat("AccelerationY", Acceleration.y);
            animator.SetBool("IsMoving", Acceleration.x != 0);
            Animator.SetBool("CanPivot", ShouldFlip());
        }

        public bool ShouldFlip()
        {
            return FacingDirection > 0 && Acceleration.x < 0 
                   || FacingDirection < 0 && Acceleration.x > 0;
        }

        public void Flip()
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            facingDirection *= -1;
        }
    }
}
