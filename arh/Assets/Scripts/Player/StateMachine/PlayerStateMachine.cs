using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private LayerMaskCollision _layerMaskCollision;
        private PlayerInput _playerInput;

        [Header("Movement")] [SerializeField] private float moveSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        [SerializeField] private float velocityPower;
        [Space] [SerializeField] private float frictionAmount;

        [Space]

        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCutMultiplier;
        [Space] [SerializeField] private float coyoteTime;
        [SerializeField] private float jumpBufferTime;
        [Space] [SerializeField] private float fallGravityMultiplier;
        [SerializeField] private float maxFallSpeed;
        
        [SerializeField] private bool _isMovementPressed;
        [SerializeField] private bool _isJumpPressed;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isFalling;
        [SerializeField] private bool _requiresNewJumpPress;
        [SerializeField] private bool _canDoubleJump;


        [SerializeField] private float _currentCoyoteTime;
        [SerializeField] private bool _isCoyoteTimeActive;

        [SerializeField] private float _currentBufferTime;
        [SerializeField] private bool _isBufferTimeActive;
        
        private PlayerStateFactory _states;
    
        
        #region Getters and Setters
        public PlayerBaseState CurrentState { get; set; }

        public Rigidbody2D Rigidbody2D { get; set; }

        public bool IsJumpPressed
        {
            get { return _isJumpPressed; }
            set => _isJumpPressed = value;
        }

        public bool IsGrounded
        {
            get { return _isGrounded; }
            set { _isGrounded = value;}
        }
        public bool IsFalling
        {
            get { return _isFalling; }
        }

        public bool RequiresNewJumpPress
        {
            get { return _requiresNewJumpPress; }
            set { _requiresNewJumpPress = value; }
        }

        public bool IsMovementPressed
        {
            get => _isMovementPressed;
            set => _isMovementPressed = value;
        }
    
        public float JumpForce => jumpForce;

        public float CoyoteTime => coyoteTime;

        public float CurrentCoyoteTime
        {
            get => _currentCoyoteTime;
            set => _currentCoyoteTime = value;
        }
        public bool OnCoyoteTime
        {
            get => _isCoyoteTimeActive;
            set => _isCoyoteTimeActive = value;
        }

        public float BufferTimer
        {
            get => jumpBufferTime;
        }

        public float CurrentBufferTime
        {
            get => _currentBufferTime;
            set => _currentBufferTime = value;
        }

        public bool OnBufferTime
        {
            get => _isBufferTimeActive;
            set => _isBufferTimeActive = value;
        }

        public bool CanDoubleJump
        {
            get => _canDoubleJump;
            set => _canDoubleJump = value;
        }

        public float MoveSpeed => moveSpeed;

        public float Acceleration => acceleration;

        public float Deceleration => deceleration;

        public float FrictionAmount => frictionAmount;
        
        public float CurrentMovementInput { get; private set; }

        public float VelocityPower => velocityPower;

        public float JumpCutMultiplier => jumpCutMultiplier;

        public float FallGravityMultiplier => fallGravityMultiplier;

        public float MaxFallSpeed => maxFallSpeed;
        
        #endregion
        
        private void Awake()
        {
            _playerInput = new PlayerInput();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _layerMaskCollision = GetComponent<LayerMaskCollision>();

            _states = new PlayerStateFactory(this);
            CurrentState = _states.Grounded();
            CurrentState.EnterState();

            _playerInput.Gameplay.Walk.started += OnMoveInput;
            _playerInput.Gameplay.Walk.canceled += OnMoveInput;
            _playerInput.Gameplay.Walk.performed += OnMoveInput;
            _playerInput.Gameplay.Jump.started += OnJumpInput;
            _playerInput.Gameplay.Jump.canceled += OnJumpInput;
            _playerInput.Gameplay.Jump.performed += OnJumpInput;
            _playerInput.Gameplay.Interact.started += OnInteractInput;
            _playerInput.Gameplay.Interact.canceled += OnInteractInput;
            _playerInput.Gameplay.Interact.performed += OnInteractInput;
        }
        
        void Start()
        {
        
        }
        
        void Update()
        {
            CurrentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            CurrentState.PhysicsUpdateStates();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            CurrentMovementInput = context.ReadValue<float>();
            _isMovementPressed = CurrentMovementInput != 0;
        }
        public void OnJumpInput(InputAction.CallbackContext context)
        {
            _isJumpPressed = context.ReadValueAsButton();
            _requiresNewJumpPress = false;
        }
        public void OnInteractInput(InputAction.CallbackContext context)
        {
        
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            _isGrounded = arg0;
        }

        private void OnEnable()
        {
            _playerInput.Gameplay.Enable();
            _layerMaskCollision.isGroundedChangedEvent.AddListener(OnIsGroundedChanged);
        }

        private void OnDisable()
        {
            _playerInput.Gameplay.Disable();
            _layerMaskCollision.isGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
        }
    }
}
