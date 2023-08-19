using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class JumpBufferHandler : MonoBehaviour
    {
        [SerializeField] private float maxJumpBuffer;
        private float _currentJumpBufferCounter;
        private bool _isJumpBuffered;

        [SerializeField] private SpaceInput spaceInput;
        [SerializeField] private PlayerJump playerJump;

        public UnityEvent<bool> JumpBufferedChangedEvent;

        private void OnEnable()
        {
            spaceInput.SpacePressedEvent.AddListener(OnSpacePressed);
            playerJump.JumpStartedEvent.AddListener(OnJumpStarted);
        }

        private void OnDisable()
        {
            spaceInput.SpacePressedEvent.RemoveListener(OnSpacePressed);
            playerJump.JumpStartedEvent.RemoveListener(OnJumpStarted);
        }

        private void OnJumpStarted()
        {
            _currentJumpBufferCounter = 0;
        }

        private void OnSpacePressed()
        {
            _currentJumpBufferCounter = maxJumpBuffer;
        }

        private void Update()
        {
            _currentJumpBufferCounter = Mathf.Clamp(_currentJumpBufferCounter - Time.deltaTime, 0, maxJumpBuffer);

            var wasJumpBuffered = _isJumpBuffered;
            _isJumpBuffered = _currentJumpBufferCounter != 0;

            if (wasJumpBuffered != _isJumpBuffered)
            {
                JumpBufferedChangedEvent.Invoke(_isJumpBuffered);
            }
        }
    }
}