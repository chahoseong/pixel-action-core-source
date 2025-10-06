using StateMachine;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D physicsBody;
        [SerializeField] private StateController stateController;

        [SerializeField] private float speed = 10.0f;

        private int facingDirection = 1;

        public Animator Animator => animator;
        public Rigidbody2D Rigidbody => physicsBody;
        public float Speed => speed;
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
            animator.SetFloat("MoveSpeed", Mathf.Abs(Rigidbody.linearVelocityX));
        }

        public void Flip()
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            facingDirection *= -1;
        }
    }
}
