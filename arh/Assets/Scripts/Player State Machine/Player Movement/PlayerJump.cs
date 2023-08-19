using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private SpaceInput spaceInput;
        [SerializeField] private CoyoteTimeHandler coyoteTimeHandler;
        [SerializeField] private JumpBufferHandler jumpBufferHandler;
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
            jumpBufferHandler.JumpBufferedChangedEvent.AddListener(OnJumpBufferedChanged);
            playerDoubleJumpCounter.HasAvailableJumpChangedEvent.AddListener(OnHasAvailableJumpChanged);
        }

        private void OnDisable()
        {
            spaceInput.SpacePressedEvent.RemoveListener(OnSpacePressed);
            spaceInput.SpaceReleasedEvent.RemoveListener(OnSpaceReleased);
            coyoteTimeHandler.CoyoteTimeActiveChangedEvent.RemoveListener(OnCoyoteTimeActiveChanged);
            jumpBufferHandler.JumpBufferedChangedEvent.RemoveListener(OnJumpBufferedChanged);
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

    public class PlayerDoubleJumpCounter : MonoBehaviour
    {
        [SerializeField] private int maxJumpCount;
        [SerializeField] private PlayerJump playerJump;
        [SerializeField] private GroundedHandler groundedHandler;
        
        private int _currentJumpCount;
        
        public UnityEvent<int> CurrentJumpCountChangedEvent;
        public UnityEvent<bool> HasAvailableJumpChangedEvent;
        private bool _isJumpAvailable;

        private void OnEnable()
        {
            playerJump.JumpStartedEvent.AddListener(OnJumpStarted);
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
        }

        private void OnDisable()
        {
            playerJump.JumpStartedEvent.RemoveListener(OnJumpStarted);
            groundedHandler.IsGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
        }

        private void OnJumpStarted()
        {
            AddJump();
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            if (arg0)
            {
                ResetJumps();
            }
        }

        public bool HasAvailableJump()
        {
            return _currentJumpCount < maxJumpCount;
        }

        private void AddJump()
        {
            _currentJumpCount += 1;
            CurrentJumpCountChangedEvent.Invoke(_currentJumpCount);
            
            var wasJumpAvailable = _isJumpAvailable;
            _isJumpAvailable = HasAvailableJump();
            if (wasJumpAvailable != _isJumpAvailable)
            {
                HasAvailableJumpChangedEvent.Invoke(_isJumpAvailable);
            }
        }

        private void ResetJumps()
        {
            _currentJumpCount = 0;
            CurrentJumpCountChangedEvent.Invoke(_currentJumpCount);
            
            var wasJumpAvailable = _isJumpAvailable;
            _isJumpAvailable = HasAvailableJump();
            if (wasJumpAvailable != _isJumpAvailable)
            {
                HasAvailableJumpChangedEvent.Invoke(_isJumpAvailable);
            }
        }
    }
}

