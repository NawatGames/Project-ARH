using System.Collections;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed of movement as needed
    private bool _canMove = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f; // Move right
        }

        if (_canMove)
        {
            // Move the player horizontally based on the input
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    public void MoveToNextRoom(int direction)
    {
        _canMove = false;

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }
    
}