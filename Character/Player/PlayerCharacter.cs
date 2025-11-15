using Abilities;
using Character.Player.States;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerCharacter : MonoBehaviour, PlayerControls.IPlayerActions, ScriptUpdateScheduler.IUpdatable,
        ScriptUpdateScheduler.ILateUpdatable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private CharacterPerception characterPerception;

        [SerializeField] private InputConfig inputConfig;
        [SerializeField] private int updateOrder = 100;
        [SerializeField] private int lateUpdateOrder = 100;
        
        [Header("Abilities")]
        [SerializeField] private AbilitySystem abilitySystem;
        [SerializeField] private AbilitySet startupAbilitySet;
        [SerializeField] private AbilityDefinition[] startupAbilities;

        [Header("Movement")]
        [SerializeField] private float wallSlidingSpeed;
        
        [Header("Debug")]
        [SerializeField, ReadOnly] private string currentState;

        private PlayerControls playerControls;
        private PlayerAnimator playerAnimator;
        
        public int UpdateOrder => updateOrder;
        public int LateUpdateOrder => lateUpdateOrder;

        public Vector2 MoveAction { get; private set; }
        public bool JumpAction { get; private set; }
        public bool AttackAction { get; private set; }

        public CharacterMovement CharacterMovement => characterMovement;
        public CharacterPerception CharacterPerception => characterPerception;
        public StateController StateController { get; } = new();
        public AbilitySystem AbilitySystem => abilitySystem;
        public PlayerAnimator PlayerAnimator => playerAnimator;
        public float WallSlidingSpeed => wallSlidingSpeed;

        private void Awake()
        {
            playerControls = new PlayerControls();
            playerAnimator = new PlayerAnimator(animator);
            
            inputConfig.Initialize();

            StateController.AddState(new StandingState(this));
            StateController.AddState(new MovingState(this));
            StateController.AddState(new PivotState(this));
            StateController.AddState(new JumpingState(this));
            StateController.AddState(new FallingState(this));
            StateController.AddState(new WallSlidingState(this));
            StateController.AddState(new WallJumpingState(this));
            StateController.AddState(new AttackState(this));
        }

        private void OnEnable()
        {
            playerControls.Enable();
            playerControls.Player.AddCallbacks(this);
        }

        private void Start()
        {
            startupAbilitySet.GiveAbilities(abilitySystem);
            StateController.ChangeState<StandingState>();
        }

        public void OnUpdate(float deltaTime)
        {
            StateController.Update(deltaTime);
            currentState = StateController.CurrentState.ToString();
        }

        public void OnLateUpdate(float deltaTime)
        {
            playerAnimator.SetVelocity(characterMovement.Velocity);
            playerAnimator.SetIsGrounded(characterMovement.IsGrounded);
        }

        private void OnDisable()
        {
            playerControls.Disable();
            playerControls.Player.RemoveCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveAction = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpAction = context.ReadValueAsButton();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            AbilitySystem.TryExecuteAbility<PrimaryAttackAbilityDefinition>();
        }
    }
}
