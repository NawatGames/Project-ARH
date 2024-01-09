using UnityEngine;
using UnityEngine.Events;

namespace Moving_Platform
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private PressurePlate relativePressurePlate;
        
        public UnityEvent playerSteppedOnPlatformEvent;
        public UnityEvent playerLeftPlatformEvent;

        public UnityEvent<bool> isPlatformMovingForwardsEvent;

        private void OnEnable()
        {
            relativePressurePlate.isOnPressurePlateChangedEvent.AddListener(OnIsOnPressurePlateChanged);
        }

        private void OnDisable()
        {
            relativePressurePlate.isOnPressurePlateChangedEvent.RemoveListener(OnIsOnPressurePlateChanged);
        }
        
        private void OnIsOnPressurePlateChanged(bool arg0)
        {
            // Debug.Log("IS ON PRESSURE PLATE: " + arg0);
            isPlatformMovingForwardsEvent.Invoke(arg0);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.transform.SetParent(transform);
            if (other.gameObject.CompareTag("Player"))
            {
                playerSteppedOnPlatformEvent.Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            other.gameObject.transform.SetParent(null);
            if (other.gameObject.CompareTag("Player"))
            {
                playerLeftPlatformEvent.Invoke();
            }
        }

        public Transform GetPlatform()
        {
            return transform;
        }
    }
}