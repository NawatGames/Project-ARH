using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class JumpBuffer : MonoBehaviour
{
    [SerializeField] private float _bufferTime = 0.1f; // testar se esse valor default está bom
    public bool isWithinBufferTime = false;

    [SerializeField] DoubleJump doubleJump;

    public UnityEvent JumpBufferedEvent;

    private void OnEnable()
    {
        doubleJump.JumpFailedEvent.AddListener(StartTimer);
    }

    private void OnDisable()
    {
        doubleJump.JumpFailedEvent.RemoveListener(StartTimer);
    }

    private IEnumerator BufferTimer(float timer)
    {
        isWithinBufferTime = true;
        Debug.Log("aaaa");
        yield return new WaitForSeconds(timer);
        isWithinBufferTime = false;

    }

    private void StartTimer()
    {
        StopAllCoroutines();
        StartCoroutine(BufferTimer(_bufferTime));
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpBufferedEvent.Invoke();
        }
    }
}
