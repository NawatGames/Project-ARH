using System;
using System.Collections;
using System.Collections.Generic;
using Player.PlayerData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AlienStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject _visualSprite;

    private LayerMaskCollision _layerMaskCollision;
    private AlienStateFactory _states;
    private PlayerInputMap _playerInput;
    
    [Space] public UnityEvent jumpCanceledEvent;
    public UnityEvent isInteractingEvent;

    #region Getters and Setters
    
    private bool _isInteractPressed { get; set; }

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
        
    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInputMap();
        Rb = GetComponent<Rigidbody2D>();
        _layerMaskCollision = GetComponent<LayerMaskCollision>();

        NormalGravityScale = Rb.gravityScale;
        CoyoteTimeCounter = playerData.coyoteTime;
        ExtraJumpsCounter = playerData.extraJumps;
        IsFacingRight = true;
            
        // Initialize StateMachine
        _states = new AlienStateFactory(this);
        CurrentState = _states.Grounded();
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
        Debug.Log("alien andando");
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
            //Debug.Log("Interagiu");
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
