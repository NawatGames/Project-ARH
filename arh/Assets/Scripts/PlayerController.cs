using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text coyote;
    public Text jumpBuffer;
    
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float velocityPower;
    [Space] [SerializeField] private float frictionAmount;

    private float _horizontalInput;
    private bool _isFacingRight;

    [Space] [Header("Jump")]
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private float jumpForce;
    [Range(0, 1)] [SerializeField] private float jumpCutMultiplier;
    [Space] [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBufferTime;
    [Space] [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float maxFallSpeed;

    private float _normalGravityScale;
    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;
    
    private bool _isFalling;
    private bool _canDoubleJump;
    
    [Space] [Header("Apex Modifiers")] 
    [SerializeField] private float jumpApexThreshold;
    // [SerializeField] private float jumpApexSpeedMultiplier;
    // [Range(0.0f, 0.9f)] [SerializeField] private float jumpApexGravityCut;

    private float _apexPoint;
    private bool _isOnApex;

    [Space] [Header("Checks")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [Space] [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _normalGravityScale = _rb.gravityScale;

        _isFacingRight = true;
    }
    
    private void Update()
    {
        _jumpBufferCounter = Mathf.Clamp(_jumpBufferCounter - Time.deltaTime, 0, jumpBufferTime);
        
        if (_isFalling && IsGrounded()) // was falling and now is grounded
        {
            _rb.gravityScale = _normalGravityScale;
            _isFalling = false;
            _canDoubleJump = canDoubleJump;
        }
        else if (IsGrounded())
        {
            _coyoteTimeCounter = coyoteTime;
        }
        else
        {
            _coyoteTimeCounter = Mathf.Clamp(_coyoteTimeCounter - Time.deltaTime, 0, coyoteTime);
        }
        
        
        if ((_horizontalInput > 0 && !_isFacingRight) || (_horizontalInput < 0 && _isFacingRight))
        {
            Flip();
        }


        coyote.text = "Coyote Time: " + _coyoteTimeCounter;
        jumpBuffer.text = "JumpBuffer: " + _jumpBufferCounter;
    }

    private void FixedUpdate()
    {
        var vel = _rb.velocity;


        #region Run

        var targetSpeed = _horizontalInput * movementSpeed;
        var speedDif = targetSpeed - _rb.velocity.x;
        var accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        var movement = Mathf.Pow(MathF.Abs(speedDif) * accelRate, velocityPower) * Mathf.Sign(speedDif);
        
        _rb.AddForce(movement * Vector2.right);
        #endregion

        
        #region Friction

        if (Mathf.Abs(_horizontalInput) < 0.01f)
        {
            var amount = Mathf.Min(Mathf.Abs(vel.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(vel.x);
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse); 
        }
        #endregion
        
        
        #region Jump
        
        if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0) // normal jump
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            _coyoteTimeCounter = 0; // prevents additional jumps
            _jumpBufferCounter = 0;
        }
        
        if (_jumpBufferCounter > 0 && _canDoubleJump && !IsGrounded() && _coyoteTimeCounter < 0) // double jump
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _canDoubleJump = false;
        
            _jumpBufferCounter = 0;
        }
        #endregion
        
        
        #region Fall Gravity + Clamped Fall Speed

        if (vel.y < 0)
        {
            _rb.gravityScale = _normalGravityScale * fallGravityMultiplier;
            _rb.velocity = new Vector2(vel.x, Mathf.Max(vel.y, -maxFallSpeed));
            _isFalling = true;
        }
        else
        {
            _rb.gravityScale = _normalGravityScale;
        }
        #endregion

        
        // #region Apex Modifiers
        //
        // _apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(vel.y));
        // var apexBonus = Mathf.Sign(_horizontalInput) * jumpApexSpeedMultiplier * _apexPoint;
        // var minFallSpeed = maxFallSpeed * (1 - jumpApexGravityCut);
        // _rb.velocity = new Vector2((apexBonus * Time.deltaTime), (Mathf.Lerp(minFallSpeed, maxFallSpeed, _apexPoint)));
        // #endregion
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpBufferCounter = jumpBufferTime;
        }
        
        if (context.canceled) // Jump Cut
        {
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<float>();
    }

    private void Flip()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        _isFacingRight = !_isFacingRight;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, groundLayer);
    }
}
