using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Moving_Platform
{
    public class PressurePlate : MonoBehaviour
    {
        private readonly HashSet<GameObject> _playersOnPressurePlate = new HashSet<GameObject>();
        private BoxCollider2D _collider;
        private bool _isOnPressurePlate;
    
        public UnityEvent<bool> isOnPressurePlateChangedEvent;

        private void OnEnable()
        {
            _collider = GetComponent<BoxCollider2D>();
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                if (_playersOnPressurePlate.Count == 0)
                {
                    _isOnPressurePlate = true;
                    isOnPressurePlateChangedEvent.Invoke(_isOnPressurePlate);
                }

                _playersOnPressurePlate.Add(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                _playersOnPressurePlate.Remove(other.gameObject);

                if (_playersOnPressurePlate.Count == 0)
                {
                    _isOnPressurePlate = false;
                    isOnPressurePlateChangedEvent.Invoke(_isOnPressurePlate);
                }
            }
        }
    }
}
