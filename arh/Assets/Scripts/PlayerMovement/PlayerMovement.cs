using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private float moveDirection;
    private int jumpCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && jumpCount == 0)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
            Debug.Log("Jump 1");
            jumpCount++;
        }

        //if(Input.GetButtonDown("Jump") && jumpCount == 1)
        //{
        //    rb.velocity = new Vector2(moveDirection * moveSpeed, jumpForce);
        //    Debug.Log("Jump 2");
        //    jumpCount++;
        //}
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Jump 0");
            jumpCount = 0;
    }
}
