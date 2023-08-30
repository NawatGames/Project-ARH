using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class JumpRequester : MonoBehaviour
    {
        [SerializeField] private SpaceInput spaceInput;
        [SerializeField] private CoyoteTimeHandler coyoteTimeHandler;
        [SerializeField] private JumpBuffer jumpBuffer;
        [SerializeField] private PlayerDoubleJumpCounter playerDoubleJumpCounter;

        private bool _isJumpBuffered;
        private bool _isCoyoteTimeActive;
        private bool _hasAvailableJump;
        
        public UnityEvent JumpStartedEvent;
        public UnityEvent JumpCanceledEvent;

        private void OnEnable()
        {
            spaceInput.SpacePressedEvent.AddListener(OnSpacePressed);
            spaceInput.SpaceReleasedEvent.AddListener(OnSpaceReleased);
            coyoteTimeHandler.CoyoteTimeActiveChangedEvent.AddListener(OnCoyoteTimeActiveChanged);
            jumpBuffer.jumpBufferedChangedEvent.AddListener(OnJumpBufferedChanged);
            playerDoubleJumpCounter.HasAvailableJumpChangedEvent.AddListener(OnHasAvailableJumpChanged);
        }

        private void OnDisable()
        {
            spaceInput.SpacePressedEvent.RemoveListener(OnSpacePressed);
            spaceInput.SpaceReleasedEvent.RemoveListener(OnSpaceReleased);
            coyoteTimeHandler.CoyoteTimeActiveChangedEvent.RemoveListener(OnCoyoteTimeActiveChanged);
            jumpBuffer.jumpBufferedChangedEvent.RemoveListener(OnJumpBufferedChanged);
            playerDoubleJumpCounter.HasAvailableJumpChangedEvent.RemoveListener(OnHasAvailableJumpChanged);
        }

        private void OnJumpBufferedChanged(bool arg0)
        {
            _isJumpBuffered = arg0;
        }

        private void OnCoyoteTimeActiveChanged(bool arg0)
        {
            _isCoyoteTimeActive = arg0;
        }

        private void OnSpaceReleased()
        {
            JumpCanceledEvent.Invoke();
        }

        private void OnHasAvailableJumpChanged(bool arg0)
        {
            _hasAvailableJump = arg0;
        }

        void OnSpacePressed()
        {
            if (_isJumpBuffered && _isCoyoteTimeActive && _hasAvailableJump)
            {
                JumpStartedEvent.Invoke();
            }
   
        }
    }
}

