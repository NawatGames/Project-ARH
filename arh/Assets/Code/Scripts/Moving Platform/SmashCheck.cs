using UnityEngine;

namespace Moving_Platform
{
    public class SmashCheck : MonoBehaviour
    {
        [SerializeField] private MovingPlatformMovementHandler movingPlatform;
        [SerializeField] private float triggerOffset;
        
        private BoxCollider2D _col;

        private void Awake()
        {
            _col = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _col.offset = Vector2.down * triggerOffset;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                movingPlatform.SetCanPlatformMove(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                movingPlatform.SetCanPlatformMove(true);
            }
        }
    }
}