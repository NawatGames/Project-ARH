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
    private SpriteRenderer _sr;
    private PlayerInput _playerInput;


    //Variables control the various actions the player can perform at any time.
    //These are fields which can are public allowing for other sctipts to read them
    //but can only be privately written to.
    public bool IsJumping { get; private set; }
    
    // Timers
    public float LastOnGroundTime { get; private set; }

    // Movement
    public int Dir { get; set; }
    
    // Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    private bool _isJumpReleased;

    // state variables
    public PlayerBaseState CurrentState { get; set; }
    private PlayerStateFactory _states;

    // classes que invocam os eventos de movimento para troca de estados
    [SerializeField] private IsGrounded _isGrounded;
    [SerializeField] private JumpRequester _jumpRequester;
    [SerializeField] private Apogee _apogee;

    // classes de evento de input
    [SerializeField] private JumpBuffer _jumpBuffer;


    // getters and setters
    
    public bool IsJumpFalling { get; private set; } 
    
    public bool IsJumpReleased
    {
        get => _isJumpReleased;
        set => _isJumpReleased = value;
    }
    
    public Rigidbody2D RB
    {
        get => _rb;
    }

    public SpriteRenderer SR
    {
        get => _sr;
    }

    #endregion
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();

        _states = new PlayerStateFactory(this, _playerInput, _isGrounded, _jumpRequester, _apogee);
        CurrentState = _states.Grounded();
        CurrentState.EnterState();

        InputAction jumpAction = _playerInput.FindAction("Jump");
        jumpAction.performed += _jumpBuffer.OnJumpInput;
        jumpAction.canceled += _jumpBuffer.OnJumpInputRelease;
    }

    private void FixedUpdate()
    {
        Run(1);
        CurrentState.FixedUpdateState();
    }

    #region RUN METHODS
    private void Run(float lerpAmount)
    {
        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = Dir * Data.runMaxSpeed;
        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
        if ((IsJumping || IsJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        //We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
        if (Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }
        #endregion

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - RB.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

        /*
		 * For those interested here is what AddForce() will do
		 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
    }
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
    #endregion
    
    #region INPUT SYSTEM HANDLER

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
