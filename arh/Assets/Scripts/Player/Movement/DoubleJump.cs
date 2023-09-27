using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class DoubleJump : MonoBehaviour
    {
        [SerializeField] private int maxNumberOfJumps;
        [SerializeField] private IsGrounded isGrounded;
        [SerializeField] private JumpSelector jumpSelector;
        
        private int _doubleJumpsLeft;
        private bool _canDoubleJump;

        public UnityEvent performDoubleJump;

        private void OnEnable()
        {
            jumpSelector.doubleJumpEvent.AddListener(TryDoubleJump);
            isGrounded.isGroundedChangedEvent.AddListener(ResetDoubleJump);
        }

        private void OnDisable()
        {
            jumpSelector.doubleJumpEvent.RemoveListener(TryDoubleJump);
            isGrounded.isGroundedChangedEvent.RemoveListener(ResetDoubleJump);
        }

        private void TryDoubleJump()
        {
            if (_doubleJumpsLeft > 0)
            {
                _doubleJumpsLeft--;
                performDoubleJump.Invoke();
            }
        }

        private void ResetDoubleJump(bool arg0)
        {
            if (!arg0) return;
            _doubleJumpsLeft = maxNumberOfJumps;
            _canDoubleJump = true;
        }
    }
}