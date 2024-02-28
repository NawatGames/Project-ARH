using UnityEngine;
using UnityEngine.Events;

public class AlienAnimationEvents : MonoBehaviour
{
    [HideInInspector] public UnityEvent alienStandUpEvent;
    [HideInInspector] public UnityEvent alienAteEvent;

    public void alienStandUpEnded()
    {
        alienStandUpEvent.Invoke();
    }

    private void AlienEatEnded()
    {
        alienAteEvent.Invoke();
    }
}
