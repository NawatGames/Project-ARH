using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class JumpBuffer : MonoBehaviour
    {
        [SerializeField] private float bufferTime;
        private float _currentBufferTimer;
        private bool _isBuffered = false;

        [SerializeField] private SpaceInput spaceInput;
        [SerializeField] private JumpRequester jumpRequester;

        public UnityEvent<bool> jumpBufferedChangedEvent;

        private void OnEnable()
        {
            spaceInput.spacePressedEvent.AddListener(OnSpacePressed);
            jumpRequester.jumpPerformedEvent.AddListener(OnJumpStarted);
        }

        private void OnDisable()
        {
            spaceInput.spacePressedEvent.RemoveListener(OnSpacePressed);
            jumpRequester.jumpPerformedEvent.RemoveListener(OnJumpStarted);
        }

        private void OnJumpStarted()
        {
            _currentBufferTimer = 0;
        }

        private void OnSpacePressed()
        {
            _currentBufferTimer = bufferTime;
        }

        private void Update()
        {
            _currentBufferTimer = Mathf.Clamp(_currentBufferTimer - Time.deltaTime, 0, bufferTime);

            var wasBuffered = _isBuffered;
            _isBuffered = _currentBufferTimer > 0;

            if (wasBuffered != _isBuffered)
            {
                jumpBufferedChangedEvent.Invoke(_isBuffered);
            }
        }
    }
}