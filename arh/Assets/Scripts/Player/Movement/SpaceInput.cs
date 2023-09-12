using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Movement
{
    public class SpaceInput : MonoBehaviour
    {
        public UnityEvent spacePressedEvent;
        public UnityEvent spaceReleasedEvent;
        
        private void SpacePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                spacePressedEvent.Invoke();
            }

            if (context.canceled)
            {
                spaceReleasedEvent.Invoke();
            }
        }
    }
}