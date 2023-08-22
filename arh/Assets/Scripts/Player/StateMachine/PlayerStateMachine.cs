using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //Scriptable object which holds all the player's movement parameters. If you don't want to use it
    //just paste in all the parameters, though you will need to manuly change all references in this script

    //HOW TO: to add the scriptable object, right-click in the project window -> create -> Player Data
    //Next, drag it into the slot in playerMovement on your player

    public PlayerData Data;
    
    #region variables
    // Components
    private Rigidbody2D _rb;
    private Collision _coll;
    private SpriteRenderer _sr;
    private PlayerInput _playerInput;
    
    //Variables control the various actions the player can perform at any time.
    //These are fields which can are public allowing for other sctipts to read them
    //but can only be privately written to.
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    
    // Timers
    public float LastOnGroundTime { get; private set; }

    // Movement
    private Vector2 _dir;
    private int _side;
    private bool _isMovementPressed;
    
    // Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    private bool _isJumpPressed;
    private bool _isJumpReleased;
    
    [Space]
    [Header("Stats")]
    [SerializeField] public float movementSpeed = 7;
    [SerializeField] public float jumpForce = 12;
    [SerializeField] public float fallMultiplier = 2.5f;
    [SerializeField] public float lowJumpMultiplier = 2f;

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
    
    public bool IsJumpFalling { get; private set; }
    
    public bool IsJumpPressed
    {
        get => _isJumpPressed;
    }
    
    public bool IsJumpReleased
    {
        get => _isJumpReleased;
        set => _isJumpReleased = value;
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

    public float getMovementSpeed
    {
        get => movementSpeed;
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
    #endregion
    
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
    }

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
    #endregion
    
    #region INPUT SYSTEM HANDLER
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
    #endregion
}
