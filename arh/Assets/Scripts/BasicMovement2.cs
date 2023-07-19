using UnityEngine;

public class BasicMovement2 : MonoBehaviour
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f; // Move right
        }

        // Move the player horizontally based on the input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}