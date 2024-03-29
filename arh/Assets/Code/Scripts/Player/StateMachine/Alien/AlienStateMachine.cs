using Player.PlayerData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AlienStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] public GameObject spriteObject;
    [HideInInspector] public Animator animator;

    [HideInInspector] public AlienAnimationEvents animationEventsScript;

    [SerializeField] private LayerMaskCollision layerMaskCollision;

    private AlienStateFactory _states;
    private PlayerInputMap _playerInput;

    [HideInInspector] public UnityEvent jumpCanceledEvent;
    [HideInInspector] public UnityEvent onEatEvent;

    public bool isCrouchPressed;
    public bool isInteractPressed;
    private string _currentAnimation;

    #region Alien Eating

    public bool hasStoredObject;
    public bool isEdibleInRange;
    public GameObject currentEdibleObject;
    public GameObject eatPointObject;

    public GameObject alienHead;
    public GameObject alienNeck;

    public float foodSize = 2; // TEMPORARIO, VAI DEPENDER DO OBJETO A ENGOLIR
    public float headMoveTime = 0.5f; // DEVERA SER CALCULADO EM FUNÇÃO DO foodSize ?
    public float spitForce = 5;

    #endregion

    #region Getters and Setters

    // Crouch
    public bool IsCrouchingPressed => isCrouchPressed;
    public float CrouchSizeReduction => playerData.crouchSizeReduction;
    public bool IsStandingUp { get; set; } = false;

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
    public float JumpApexThreshold => playerData.jumpApexThreshold;
    public float ApexBonus => playerData.apexBonus;
    public float NormalGravityScale { get; private set; }
    public bool IsGrounded { get; private set; }

    // Counters
    public float CoyoteTimeCounter { get; set; }
    public float JumpBufferCounter { get; set; }
    public int ExtraJumpsCounter { get; set; }

    public GameObject Sprite => spriteObject;
    public AlienBaseState CurrentState { get; set; }
    public Rigidbody2D Rb { get; private set; }
    public CapsuleCollider2D CapsuleCollider { get; set; }
    public LayerMaskCollision LmCollision => layerMaskCollision;

    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInputMap();
        Rb = GetComponent<Rigidbody2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        layerMaskCollision = GetComponent<LayerMaskCollision>();
        animator = spriteObject.GetComponent<Animator>();
        animationEventsScript = spriteObject.GetComponent<AlienAnimationEvents>();

        NormalGravityScale = Rb.gravityScale;
        CoyoteTimeCounter = playerData.coyoteTime;
        ExtraJumpsCounter = playerData.extraJumps;
        IsFacingRight = true;
        
        // Initialize StateMachine
        _states = new AlienStateFactory(this);
        CurrentState = _states.Grounded();
        CurrentState.InitializeSubState();
        CurrentState.EnterState();
    }

    private void Update()
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        CurrentState.UpdateStates();
        JumpBufferCounter = Mathf.Clamp(JumpBufferCounter - Time.deltaTime, 0, playerData.jumpBufferTime);
    }

    private void FixedUpdate()
    {
        CurrentState.PhysicsUpdateStates();
    }

    public void OnWalkInput(InputAction.CallbackContext context)
    {
        //Debug.Log("alien andando");
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

    public void OnEatInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInteractPressed = context.ReadValueAsButton();
            onEatEvent.Invoke();
            //Debug.Log("OnEatInput");
        }

        if (context.canceled)
        {
            isInteractPressed = false;
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouchPressed = context.ReadValueAsButton();
        }
        if (context.canceled)
        {
            isCrouchPressed = false;
        }
    }

    private void OnEnable()
    {
        _playerInput.AlienGameplay.Enable();
        layerMaskCollision.isGroundedChangedEvent.AddListener(OnIsGroundedChanged);
    }

    private void OnDisable()
    {
        _playerInput.AlienGameplay.Disable();
        layerMaskCollision.isGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
    }
    
    private void OnIsGroundedChanged(bool arg0)
    {
        //Debug.Log("Gr sm");
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
        if (_currentAnimation == newAnimation) return;
        animator.Play(newAnimation, -1, 0f);
        _currentAnimation = newAnimation;
    }
}
