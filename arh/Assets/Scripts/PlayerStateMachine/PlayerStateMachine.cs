using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // state variables
    private PlayerBaseState _currentstate;
    private PlayerStateFactory _states;
    
    // getters and setters
    public PlayerBaseState CurrentState
    {
        get => _currentstate;
        set => _currentstate = value;
    }
    public Vector2 CurrentDir
    {
        get => _dir;
        set => _dir = value;
    }
    public bool IsMoving
    {
        get =>_isMoving;
        set => _isMoving = value;
    }

    public bool IsJumping => _isJumping;
    public Collision GetColl => _coll;
    public Rigidbody2D GetRb => _rb;
    public float CurrentSpeed
    {
        get => speed;
        set => speed = value;
    }
    public float CurrentJumpForce
    {
        get => jumpForce;
        set => jumpForce = value;
    }
    public float CurrentWallJumpLerp => wallJumpLerp;
    public float CurrentFallMult
    {
        get => fallMultiplier;
        set => fallMultiplier = value;
    }
    public float CurrentLowJumpMult
    {
        get => lowJumpMultiplier;
        set => lowJumpMultiplier = value;
    }

    public bool CanMove => canMove;
    
    private Collision _coll;
    [HideInInspector]
    private Rigidbody2D _rb;

    // Vector2 'direction'
    private Vector2 _dir;
    
    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float wallJumpLerp = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    
    
    [Space]
    [Header("Booleans")]
    public bool canMove;

    [Space] 
    private bool _isMoving;
    private bool _isJumping;

    public int side = 1;

    void Awake()
    {
        _coll = GetComponent<Collision>();
        _rb = GetComponent<Rigidbody2D>();
        _states = new PlayerStateFactory(this);
        _currentstate = _states.Grounded();
        _currentstate.EnterState();
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentstate.UpdateStates();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        _isMoving = context.ReadValueAsButton();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumping = context.ReadValueAsButton();
    }
}
