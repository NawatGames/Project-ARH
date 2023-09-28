using UnityEngine;
using UnityEngine.Events;

public class LayerMaskCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]
    private bool _isGrounded;

    [Space]
    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color _debugCollisionColor = Color.red;

    public UnityEvent<bool> isGroundedChangedEvent;
    
    private void Update()
    {  
        var wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        if (wasGrounded != _isGrounded)
        {
            isGroundedChangedEvent.Invoke(_isGrounded);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        var pos = transform.position;
        Gizmos.DrawWireSphere((Vector2)pos  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)pos + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)pos + leftOffset, collisionRadius);
    }
}