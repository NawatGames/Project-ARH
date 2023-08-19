using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class CoyoteTimeHandler : MonoBehaviour
    {
        [SerializeField] private float maxCoyoteTime;
        [SerializeField] private GroundedHandler groundedHandler;
        private float _currentCoyoteTime;
        private bool _isGrounded;

        private bool _isCoyoteTimeActive;
        [SerializeField] private PlayerJump playerJump;

        public UnityEvent<bool> CoyoteTimeActiveChangedEvent;

        private void OnEnable()
        {
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
            playerJump.JumpStartedEvent.AddListener(OnJumpStarted);
        }
        
        private void OnDisable()
        {
            groundedHandler.IsGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
            playerJump.JumpStartedEvent.RemoveListener(OnJumpStarted);
        }

        private void OnJumpStarted()
        {
            _currentCoyoteTime = 0;
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            _isGrounded = arg0;
            
            if (arg0)
            {
                _currentCoyoteTime = maxCoyoteTime;
            }
        }
        
        private void Update()
        {
            if (!_isGrounded)
            {
                _currentCoyoteTime = Mathf.Clamp(_currentCoyoteTime - Time.deltaTime, 0f, maxCoyoteTime);
            }

            var wasCoyoteTimeActive = _isCoyoteTimeActive;
            _isCoyoteTimeActive = _currentCoyoteTime > 0;

            if (wasCoyoteTimeActive != _isCoyoteTimeActive)
            {
                CoyoteTimeActiveChangedEvent.Invoke(_isCoyoteTimeActive);
            }
        }
    }
}