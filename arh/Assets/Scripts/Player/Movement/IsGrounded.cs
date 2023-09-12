using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class IsGrounded : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private Transform groundCheckPoint;

        public UnityEvent<bool> isGroundedChangedEvent;

        private bool _isGrounded;

        private void FixedUpdate()
        {
            var wasGrounded = _isGrounded;
            _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

            if (wasGrounded != _isGrounded)
            {
                isGroundedChangedEvent.Invoke(_isGrounded);
            }
        }
    }
}