using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class CoyoteTime : MonoBehaviour
    {
        [SerializeField] private float coyoteTime;
        [SerializeField] private IsGrounded isGrounded;
        [SerializeField] private JumpRequester jumpRequester;

        private float _coyoteTimer;
        private bool _isCoyoteTimeActive;
        private bool _isGrounded;

        public UnityEvent<bool> coyoteTimeActiveChangedEvent;

        private void OnEnable()
        {
            isGrounded.isGroundedChangedEvent.AddListener(OnIsGroundedChanged);
            jumpRequester.jumpPerformedEvent.AddListener(OnJumpPerformedEvent);
        }

        private void OnDisable()
        {
            isGrounded.isGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
            jumpRequester.jumpPerformedEvent.RemoveListener(OnJumpPerformedEvent);
        }

        private void Update()
        {
            _coyoteTimer = Mathf.Clamp(_coyoteTimer - Time.deltaTime, 0f, coyoteTime);
            
            var wasCoyoteTimeActive = _isCoyoteTimeActive;
            _isCoyoteTimeActive = _coyoteTimer > 0;

            if (wasCoyoteTimeActive != _isCoyoteTimeActive)
            {
                coyoteTimeActiveChangedEvent.Invoke(_isCoyoteTimeActive);
            }
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            if (arg0)
            {
                _coyoteTimer = coyoteTime;
            }
        }

        private void OnJumpPerformedEvent()
        {
            _coyoteTimer = 0;
            // coyoteTimeActiveChangedEvent.Invoke(false);
        }
    }
}