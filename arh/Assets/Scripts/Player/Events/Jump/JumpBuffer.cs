using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class JumpBuffer : MonoBehaviour
{
    [SerializeField] JumpRequester jumpRequester; // Para chamar funcao quando jumpInput for solto

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
        // if(context.performed)    (desnecessário pois o listener é adicionado direto no performed (em PlayerStateMachine))
        JumpBufferedEvent.Invoke();
    }

    public void OnJumpInputRelease(InputAction.CallbackContext context)
    {
        jumpRequester.OnJumpInputReleased();
    }
}
