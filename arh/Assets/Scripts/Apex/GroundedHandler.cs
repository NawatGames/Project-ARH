using UnityEngine;
using UnityEngine.Events;

public class GroundedHandler : MonoBehaviour
{
    private bool _isGrounded;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    public UnityEvent<bool> IsGroundedChangedEvent;
    
    void Update()
    {
        var wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, groundLayer);

        if (wasGrounded != _isGrounded)
        {
            IsGroundedChangedEvent.Invoke(_isGrounded);
        }
    }
}
