using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class PlayerDoubleJumpCounter : MonoBehaviour
    {
        [SerializeField] private int maxJumpCount;
        [SerializeField] private JumpRequester jumpRequester;
        [SerializeField] private GroundedHandler groundedHandler;
        
        private int _currentJumpCount;
        
        public UnityEvent<int> CurrentJumpCountChangedEvent;
        public UnityEvent<bool> HasAvailableJumpChangedEvent;
        private bool _isJumpAvailable;

        private void OnEnable()
        {
            jumpRequester.JumpStartedEvent.AddListener(OnJumpStarted);
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
        }

        private void OnDisable()
        {
            jumpRequester.JumpStartedEvent.RemoveListener(OnJumpStarted);
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