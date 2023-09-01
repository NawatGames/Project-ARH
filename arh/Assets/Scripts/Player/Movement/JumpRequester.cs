using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class JumpRequester : MonoBehaviour
    {
        [SerializeField] private DoubleJump doubleJump;
        [SerializeField] private JumpSelector jumpSelector;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float jumpForce;

        public UnityEvent jumpPerformedEvent;

        private void OnEnable()
        {
            jumpSelector.jumpEvent.AddListener(OnJump);
            jumpSelector.doubleJumpEvent.AddListener(OnDoubleJump);
        }

        private void OnDisable()
        {
            jumpSelector.jumpEvent.RemoveListener(OnJump);
            jumpSelector.doubleJumpEvent.RemoveListener(OnDoubleJump);
        }

        private void OnJump()
        {
            jumpPerformedEvent.Invoke();
            PerformJump();
        }

        private void OnDoubleJump()
        {
            jumpPerformedEvent.Invoke();
            PerformJump();
        }

        private void PerformJump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}