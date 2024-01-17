using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Code.Scripts.Player.StateMachine.Astronaut
{
    public class AstronautStateMachine : MonoBehaviour
    {
        
        [SerializeField] private global::Player.PlayerData.PlayerData playerData;
        [SerializeField] private GameObject sprite;
        [HideInInspector] public Animator animator;

        private LayerMaskCollision _layerMaskCollision;
        private AstronautStateFactory _states;
        private PlayerInputMap _playerInput;
        private string currentAnimation;

        [HideInInspector] public UnityEvent jumpCanceledEvent;
        [HideInInspector] public UnityEvent isInteractingEvent;
        
        #region Getters and Setters
        private bool IsInteractPressed { get; set; }
        
        // Movement
        public float MoveSpeed => playerData.moveSpeed;
        public float Acceleration => playerData.acceleration;
        public float Deceleration => playerData.deceleration;
        public float VelocityPower => playerData.velocityPower;
        public float FrictionAmount => playerData.frictionAmount;
        public float CurrentMovementInput { get; private set; }
        public bool IsFacingRight { get; set; }

        // Jump
        public float JumpForce => playerData.jumpForce;
        public float JumpCutMultiplier => playerData.jumpCutMultiplier;
        public float FallGravityMultiplier => playerData.fallGravityMultiplier;
        public float MaxFallSpeed => playerData.maxFallSpeed;
        public float NormalGravityScale { get; private set; }
        public bool IsGrounded { get; private set; }

        // Counters
        public float CoyoteTimeCounter { get; set; }
        public float JumpBufferCounter { get; set; }
        public int ExtraJumpsCounter { get; set; }

        public GameObject Sprite => sprite;
        public AstronautBaseState CurrentState { get; set; }
        public Rigidbody2D Rb { get; private set; }
        public SoundEffectAudioPlayer audioPlayer { get; private set; }
        
        #endregion
        
        private void Awake()
        {
            _playerInput = new PlayerInputMap();
            Rb = GetComponent<Rigidbody2D>();
            audioPlayer = GetComponent<SoundEffectAudioPlayer>();
            _layerMaskCollision = GetComponent<LayerMaskCollision>();
            animator = sprite.GetComponent<Animator>();

            NormalGravityScale = Rb.gravityScale;
            CoyoteTimeCounter = playerData.coyoteTime;
            ExtraJumpsCounter = playerData.extraJumps;
            IsFacingRight = true;
            
            // Initialize StateMachine
            _states = new AstronautStateFactory(this);
            CurrentState = _states.Grounded();
            CurrentState.InitializeSubState();
            CurrentState.EnterState();
        }

        private void Update()
        {
            CurrentState.UpdateStates();
            JumpBufferCounter = Mathf.Clamp(JumpBufferCounter - Time.deltaTime, 0, playerData.jumpBufferTime);
        }

        private void FixedUpdate()
        {
            CurrentState.PhysicsUpdateStates();
        }

        public void OnWalkInput(InputAction.CallbackContext context)
        {        
            //Debug.Log("astronauta andando");
            CurrentMovementInput = context.ReadValue<float>();
        }
        
        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpBufferCounter = playerData.jumpBufferTime;
            }

            if (context.canceled)
            {
                jumpCanceledEvent.Invoke();
            }
        }
        
        public void OnInteractInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsInteractPressed = context.ReadValueAsButton();
                isInteractingEvent.Invoke();
                 //Debug.Log("Astronauta Interagiu");
            }
        }

        private void OnEnable()
        {
            _playerInput.AstronautGameplay.Enable();
            _layerMaskCollision.isGroundedChangedEvent.AddListener(OnIsGroundedChanged);
        }

        private void OnDisable()
        {
            _playerInput.AstronautGameplay.Disable();
            _layerMaskCollision.isGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
        }
        
        private void OnIsGroundedChanged(bool arg0)
        {
            IsGrounded = arg0;
        }

        public void ResetCoyoteTime()
        {
            CoyoteTimeCounter = playerData.coyoteTime;
        }

        public void ResetJumpCount()
        {
            ExtraJumpsCounter = playerData.extraJumps;
        }

        public void SetVelocity(float x, float y)
        {
            Rb.velocity = new Vector2(x, y);
        }

        public void ChangeAnimation(string newAnimation)
        {
            if (newAnimation == "AstronautAscending")
            {
                animator.Play(newAnimation, -1, 0f);
                currentAnimation = newAnimation;
                return;
            }
            
            if (currentAnimation == newAnimation) return;
            
            animator.Play(newAnimation, -1, 0f);
            currentAnimation = newAnimation;
        }
    }
}
