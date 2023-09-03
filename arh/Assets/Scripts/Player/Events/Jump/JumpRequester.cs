using UnityEngine;
using UnityEngine.Events;

public class JumpRequester : MonoBehaviour
{
    [SerializeField] private DoubleJump _doubleJump;
    [SerializeField] private JumpSelector _jumpSelector;

    //Evento de pulo final: fazer o sistema de pulo, para ouvir esse evento e toda vez q ele for disparado adicionar uma velocidade vertical ao player.
    // Coyote também ouve esse evento para diferenciar uma queda de um pulo (os dois invocam onNotGrounded)
    public UnityEvent PerformJumpEvent;
    public UnityEvent ReleasedJumpEvent;


    private void OnEnable()
    {
        _jumpSelector.JumpEvent.AddListener(OnJump);
        _jumpSelector.JumpBufferedEvent.AddListener(OnBufferedJump);
        _doubleJump.PerformDoubleJump.AddListener(OnDoubleJump);
    }

    private void OnDisable()
    {
        _jumpSelector.JumpEvent.RemoveListener(OnJump);
        _jumpSelector.JumpBufferedEvent.RemoveListener(OnBufferedJump);
        _doubleJump.PerformDoubleJump.RemoveListener(OnDoubleJump);
    }

    private void OnJump()
    {
        //Debug.Log("SingleJump");
        PerformJumpEvent.Invoke();
    }

    private void OnBufferedJump()
    {
        //Debug.Log("BufferedJump");
        PerformJumpEvent.Invoke();
    }

    private void OnDoubleJump()
    {
        //Debug.Log("Double");
        PerformJumpEvent.Invoke();
    }

    public void OnJumpInputReleased()
    {
        ReleasedJumpEvent.Invoke();
    }
}