using UnityEngine;
using UnityEngine.Events;

public class AlienAnimationEvents : MonoBehaviour
{
    [HideInInspector] public UnityEvent alienStandUpEvent;

    public void alienStandUpEnded()
    {
        alienStandUpEvent.Invoke();
    }
}
