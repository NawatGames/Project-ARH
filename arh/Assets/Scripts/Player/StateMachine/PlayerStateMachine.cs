using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private Collision _collisionContext;
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;

    [SerializeField] private Vector2 _currentMovementInput;
    [SerializeField] Vector2 _currentMovement;
    [SerializeField] private float _appliedMovementSpeed;
    [SerializeField] private float _appliedJumpForce;

    [SerializeField] private bool _isMovementPressed;
    [SerializeField] private bool _isJumpPressed;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isFalling;
    [SerializeField] private bool _requiresNewJumpPress;


    [SerializeField] private float _coyoteTimer;
    [SerializeField] private float _actualCoyoteTime;
    [SerializeField] private bool _isCoyoteTimeActive;
    
    
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    
    //Getters and Setters
    public PlayerBaseState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    public Rigidbody2D Rigidbody2D
    {
        get { return _rb; }
        set { _rb = value; }
    }
    public bool IsJumpPressed
    {
        get { return _isJumpPressed; }
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

    public Vector2 CurrentMovement
    {
        get { return _currentMovement;}
        set { _currentMovement = value;}
    }
    
    public Vector2 CurrentMovementInput
    {
        get { return _currentMovementInput; }
    }
    public float AppliedMovementSpeed
    {
        get { return _appliedMovementSpeed; }
    }
    public bool IsMovementPressed
    {
        get { return _isMovementPressed; }
        set { _isMovementPressed = value; }
    }
    
    public float AppliedJumpForce
    {
        get { return _appliedJumpForce; }
    }

    public float CoyoteTimer
    {
        get { return _coyoteTimer; }
    }

    public float ActualCoyoteTime
    {
        get => _actualCoyoteTime;
        set => _actualCoyoteTime = value;
    }
    public bool OnCoyoteTime
    {
        get => _isCoyoteTimeActive;
        set => _isCoyoteTimeActive = value;
    }


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
        _collisionContext = GetComponent<Collision>();


        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();


        _playerInput.Gameplay.Walk.started += OnMomeventInput;
        _playerInput.Gameplay.Walk.canceled += OnMomeventInput;
        _playerInput.Gameplay.Walk.performed += OnMomeventInput;
        _playerInput.Gameplay.Jump.started += OnJumpInput;
        _playerInput.Gameplay.Jump.canceled += OnJumpInput;
        _playerInput.Gameplay.Jump.performed += OnJumpInput;
        _playerInput.Gameplay.Interact.started += OnInteractInput;
        _playerInput.Gameplay.Interact.canceled += OnInteractInput;
        _playerInput.Gameplay.Interact.performed += OnInteractInput;



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
        _isGrounded = _collisionContext.onGround;
        _isFalling = _rb.velocity.y <= 0.0f;
    }

    private void FixedUpdate()
    {
        _currentState.PhysicsUpdateStates();
    }

    public void OnMomeventInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requiresNewJumpPress = false;
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Disable();
    }
}
