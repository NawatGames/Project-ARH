using System;
using UnityEngine;
using UnityEngine.Events;

public class LayerMaskCollision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool _isGrounded;

    public bool _isHittingRoof;
    
    public Vector2 size;


    [Space]
    [Header("Collision")]

    public float collisionRadius = 0.25f;

    public Vector2 bottomOffset, rightOffset, leftOffset, topOffset;

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

        var wasHittingRoof = _isHittingRoof;
        _isHittingRoof = Physics2D.OverlapBox((Vector2)transform.position + topOffset, size, 0f, groundLayer);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset, topOffset };

        var pos = transform.position;
        Gizmos.DrawWireSphere((Vector2)pos  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)pos + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)pos + leftOffset, collisionRadius);
        
        Gizmos.DrawWireCube((Vector2)pos + topOffset, size);
    }
}