using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontalMovement;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Rigidbody2D _rb;
    private bool _canDoubleJump;
    [SerializeField] private CheckGround _checkGroundScript;
    [SerializeField] private float _horizontalMovementSpeed;

    private void OnEnable()
    {
        _checkGroundScript.OnGroundedEvent.AddListener(OnGrounded);
    }

    private void OnDisable()
    {
        _checkGroundScript.OnGroundedEvent.RemoveListener(OnGrounded);
    }

    private void OnGrounded()
    {
        _canDoubleJump = true;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalMovement * _horizontalMovementSpeed, _rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && _canDoubleJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
            _canDoubleJump = !_canDoubleJump;
        }
    }


}
