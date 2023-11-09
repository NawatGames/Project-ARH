using System;
using System.Collections;
using System.Collections.Generic;
using Player.PlayerData;
using Player.StateMachine.Alien;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AlienStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject _visualSprite;
    [HideInInspector] public Animator animator;

    [HideInInspector] public AlienAnimationEvents animationEventsScript;
    
    [SerializeField] private LayerMaskCollision _layerMaskCollision;

    private CapsuleCollider2D _boxCollider;
    private AlienStateFactory _states;
    private PlayerInputMap _playerInput;

    [HideInInspector] public UnityEvent jumpCanceledEvent;
    [HideInInspector] public UnityEvent isInteractingEvent;


    public bool _isCrouchPressed;
    public bool _isInteractPressed;

    
    #region Getters and Setters

    // Crouch
    public bool IsCrouchingPressed => _isCrouchPressed;
    public float _crouchSizeReduction => playerData.crouchSizeReduction;
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

    public GameObject Sprite => _visualSprite;
    public AlienBaseState CurrentState { get; set; }
    public Rigidbody2D Rb { get; private set; }
    public CapsuleCollider2D BoxCollider { get; set; }
    public LayerMaskCollision LmCollision => _layerMaskCollision;

    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInputMap();
        Rb = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<CapsuleCollider2D>();
        _layerMaskCollision = GetComponent<LayerMaskCollision>();
        animator = _visualSprite.GetComponent<Animator>();
        animationEventsScript = _visualSprite.GetComponent<AlienAnimationEvents>();

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
    
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isInteractPressed = context.ReadValueAsButton();
            isInteractingEvent.Invoke();
            //Debug.Log("Alien Interagiu");
        }

        if (context.canceled)
        {
            _isInteractPressed = false;
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isCrouchPressed = context.ReadValueAsButton();
        }
        if (context.canceled)
        {
            _isCrouchPressed = false;
        }
    }
    
    private void OnEnable()
    {
        _playerInput.AlienGameplay.Enable();
        _layerMaskCollision.isGroundedChangedEvent.AddListener(OnIsGroundedChanged);
    }

    private void OnDisable()
    {
        _playerInput.AlienGameplay.Disable();
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
}
