using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Moving_Platform
{
    public class MovingPlatformMovementHandler : MonoBehaviour
    {
        [SerializeField] private Transform finalPosition;
        [SerializeField] private float speed;

        private MovingPlatform _movingPlatform;

        private Vector2 _startPos;
        private Vector2 _movementDirection;

        private bool _movingForwards;
        private bool _canPlatformMove = true;

        private Transform _platform;

        private void Awake()
        {
            _movingPlatform = GetComponent<MovingPlatform>();
            _platform = _movingPlatform.GetPlatform();
            _startPos = transform.position;
        }

        private void OnEnable()
        {
            _movingPlatform.isPlatformMovingForwardsEvent.AddListener(OnIsPlatformMovingForwards);
        }

        private void OnDisable()
        {
            _movingPlatform.isPlatformMovingForwardsEvent.RemoveListener(OnIsPlatformMovingForwards);
        }

        private void OnIsPlatformMovingForwards(bool arg0)
        {
            // Debug.Log("IS PLATFORM MOVING FORWARDS: " + arg0);
            _movingForwards = arg0;
        }

        private void FixedUpdate()
        {
            var platformPos = _platform.position;
            var finalPos = finalPosition.position;
            var step = speed * Time.deltaTime;

            if (_movingForwards && _canPlatformMove) // going towards the final position
            {
                if (Vector3.Distance(platformPos, finalPos) < 0.01f)
                {
                    step = 0; // stop
                    _movementDirection = Vector2.zero;
                }
                else _movementDirection = (finalPos - platformPos).normalized;
                
                _platform.position = Vector2.MoveTowards(platformPos, finalPos, step);
            }
            else if (_canPlatformMove) // going towards the start position
            {
                if (Vector3.Distance(platformPos, _startPos) < 0.01f)
                {
                    step = 0; // stop
                    _movementDirection = Vector2.zero;
                }
                else _movementDirection = (_startPos - (Vector2)platformPos).normalized;
                
                _platform.position = Vector2.MoveTowards(platformPos, _startPos, step);
            }
        }

        public Vector2 GetMovementDirection()
        {
            return _movementDirection;
        }

        public bool GetCanPlatformMove()
        {
            return _canPlatformMove;
        }

        public void SetCanPlatformMove(bool value)
        {
            _canPlatformMove = value;
        }
    }
}