using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class SpaceInput : MonoBehaviour
    {
        public UnityEvent SpacePressedEvent;
        public UnityEvent SpaceReleasedEvent;
        
        private void Update()
        {
            var keyDown = Input.GetKeyDown("Space");
            var keyUp = Input.GetKeyUp("Space");
            
            if (keyDown)
            {
                SpacePressedEvent.Invoke();
            }

            if (keyUp)
            {
                SpaceReleasedEvent.Invoke();
            }
        }
    }
}