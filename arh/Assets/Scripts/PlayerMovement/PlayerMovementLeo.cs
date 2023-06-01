using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLeo: MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    private float _movementDirection;
    private float _speed = 5f;
    

    [SerializeField] private float _jumpPower = 16f;
    [SerializeField] private Transform _feetPosition;
    [SerializeField] private LayerMask _groundLayer;
    public float checkRadius;
    [SerializeField] private bool _isGrounded;

    
    
    // Start is called before the first frame update
    void Start()
    {
        this._rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        _movementDirection = Input.GetAxisRaw("Horizontal");

        if (_movementDirection != 0)
        {
            _rb.velocity = new Vector2(_movementDirection * _speed, _rb.velocity.y);
        }
        
        
        
        
        _isGrounded = Physics2D.OverlapCircle(_feetPosition.position, checkRadius, _groundLayer);

        
        if (_isGrounded == true && Input.GetButton("Jump"))
        {
            _rb.velocity = Vector2.up * _jumpPower; 
        }

        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }
    
}
