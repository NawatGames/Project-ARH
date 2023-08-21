using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private float moveDirection;
    private Transform groundCheck;
    private LayerMask groundLayer;
    private bool doublejumpEnabled;
    private bool isGrounded;
    public bool jetpackEnabled;

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

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
                isGrounded = false;
            }

            else if(doublejumpEnabled)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
                doublejumpEnabled = false;
            }
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
