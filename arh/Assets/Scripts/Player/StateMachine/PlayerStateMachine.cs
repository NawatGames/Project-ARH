using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collision _coll;
    private SpriteRenderer _sr;
    private PlayerInput _playerInput;
    
    private Vector2 _dir;
    private int _side;
    
    private bool _isMovementPressed;
    private bool _isJumpPressed;
    
    [Space]
    [Header("Stats")]
    public float movementSpeed = 10;
    public float jumpForce = 50;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // state variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    // getters and setters
    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value; 
    }

    public bool IsMovementPressed
    {
        get => _isMovementPressed;
    }
    
    public bool IsJumpPressed
    {
        get => _isJumpPressed;
    }

    public Vector2 getDir
    {
        get => _dir;
    }
    
    public Rigidbody2D getRB
    {
        get => _rb;
    }
    
    public Collision getColl
    {
        get => _coll;
    }

    public SpriteRenderer getSR
    {
        get => _sr;
    }
    
    public float getFallMultiplier
    {
        get => fallMultiplier;
    }
    
    public float getLowJumpMultiplier
    {
        get => lowJumpMultiplier;
    }

    public float getJumpForce
    {
        get => jumpForce;
    }

    public int getSide
    {
        get => _side;
        set => _side = value;
    }
    
    private void Awake()
    {
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
        
        _playerInput = new PlayerInput();
        
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collision>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _currentState.UpdateStates();
        Walk(_dir); 
    }

    private void Walk(Vector2 dir)
    {
        _rb.velocity = new Vector2(dir.x * movementSpeed, _rb.velocity.y);
    }
    
    // callback handler function to set the player input values
    public void OnWalkInput(InputAction.CallbackContext context)
    {
        _dir = context.ReadValue<Vector2>();
        _isMovementPressed = _dir.x != 0f || _dir.y != 0f;
    }
    
    // callback handler function for jump buttons
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
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
