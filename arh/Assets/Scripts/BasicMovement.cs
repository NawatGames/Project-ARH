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
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player horizontally based on the input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}