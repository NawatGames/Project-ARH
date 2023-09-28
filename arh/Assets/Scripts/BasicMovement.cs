using TMPro;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed of movement as needed

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
        else if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * 0.05f, ForceMode2D.Impulse);
        }

        // Move the player horizontally based on the input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}