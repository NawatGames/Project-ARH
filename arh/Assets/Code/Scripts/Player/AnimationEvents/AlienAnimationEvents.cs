using UnityEngine;
using UnityEngine.Events;

public class AlienAnimationEvents : MonoBehaviour
{
    [HideInInspector] public UnityEvent alienStandUpEvent;
    [HideInInspector] public UnityEvent alienAteEvent;
    [HideInInspector] public UnityEvent alienEatStartEndEvent;

    public void AlienStandUpEnded()
    {
        alienStandUpEvent.Invoke();
    }

    private void AlienEatStartEnded()
    {
        alienEatStartEndEvent.Invoke();
    }
    
    private void AlienEatEnded()
    {
        alienAteEvent.Invoke();
    }
}
