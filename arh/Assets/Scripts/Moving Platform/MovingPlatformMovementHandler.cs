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

        private Vector2 _startPosition;
        private Vector2 _movementDirection;

        private bool _movingForwards;
        private Transform _platform;
        
        private void Awake()
        {
            _movingPlatform = GetComponent<MovingPlatform>();
            _platform = _movingPlatform.GetPlatform();
            _startPosition = transform.position;
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
            var step = speed * Time.deltaTime;
            
            if (_movingForwards)
            {
                if (Vector3.Distance(_platform.position, finalPosition.position) < 0.01f)
                {
                    step = 0;
                }
                _platform.position = Vector2.MoveTowards(_platform.position, finalPosition.position, step);
            }
            else
            {
                if (Vector3.Distance(_platform.position, _startPosition) < 0.01f)
                {
                    step = 0;
                }
                _platform.position = Vector2.MoveTowards(_platform.position, _startPosition, step);
            }
        }
    }
}