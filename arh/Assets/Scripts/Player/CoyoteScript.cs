using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoyoteScript : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private float moveDirection;
    private Transform groundCheck;
    private LayerMask groundLayer;
    private bool doublejumpEnabled;
    public bool isGrounded;
    public bool jetpackEnabled;
    private float coyoteTime = 0.2f;
    public float coyoteCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public InputAction playerControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        doublejumpEnabled = false;
        jetpackEnabled = false;
    }

    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if(jumpBufferCounter > 0f)
        {
            if(coyoteCounter > 0f)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
                //isGrounded = false;
                jumpBufferCounter = 0f;
            }

            else if(doublejumpEnabled)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
                doublejumpEnabled = false;
            }
        }

        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpBufferCounter = jumpBufferTime;
        }

        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                //comentar ou remover o de cima se não tiver mecânica de alterar força de pulo de acordo com input
                coyoteCounter = 0f;
            }
        }

        if(isGrounded)
        {
            coyoteCounter = coyoteTime;
        }

        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if(jetpackEnabled)
        {
            doublejumpEnabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("JetpackPickup"))
        {
            jetpackEnabled = true;
            doublejumpEnabled = true;
            Destroy(col.gameObject);
        }
    }
}
