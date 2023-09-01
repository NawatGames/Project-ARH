using UnityEngine;
using UnityEngine.Events;

public class JumpSelector : MonoBehaviour
{
    public UnityEvent JumpEvent;
    public UnityEvent JumpBufferedEvent;
    public UnityEvent DoubleJumpEvent;
    public UnityEvent JumpCanceledEvent;

    [SerializeField] private JumpBuffer _jumpBuffer;
    [SerializeField] private Coyote _coyote;
    [SerializeField] private IsGrounded _isGrounded;

    private bool _isCoyoteActive;

    private void OnEnable()
    {
        _jumpBuffer.JumpBufferedEvent.AddListener(OnJumpBuffered);
        _coyote.CoyoteOrGroundStartEvent.AddListener(OnCoyoteStart);
        _coyote.CoyoteOrGroundEndEvent.AddListener(OnCoyoteEnd);
        _isGrounded._onGrounded.AddListener(RetryBufferedJump);
    }

    private void OnDisable()
    {
        _jumpBuffer.JumpBufferedEvent.RemoveListener(OnJumpBuffered);
        _coyote.CoyoteOrGroundStartEvent.RemoveListener(OnCoyoteStart);
        _coyote.CoyoteOrGroundEndEvent.RemoveListener(OnCoyoteEnd);
        _isGrounded._onGrounded.RemoveListener(RetryBufferedJump);
    }

    private void OnJumpBuffered()
    {
        if (_isCoyoteActive)
        {
            JumpEvent.Invoke();
            //pensar numa maneira para resetar o buffer de pulo quando o player pular de fato, e desativar o coyote, para não permitir multiplos pulos
        }
        else
        {
            DoubleJumpEvent.Invoke();
            //pensar numa maneira para resetar o buffer de pulo quando o player pular de fato, e desativar o coyote, para não permitir multiplos pulos
        }
    }

    private void RetryBufferedJump() // Tenta novamente executar um pulo que falhou (usando o JumpBuffer)
    {
        if(_jumpBuffer.isWithinBufferTime)
        {
            Debug.Log("buffer");
            _jumpBuffer.isWithinBufferTime = false; // Não precisa esperar o timer acabar, já seta false agora
            JumpBufferedEvent.Invoke();
        }
    }

    private void OnCoyoteStart()
    {
        _isCoyoteActive = true;
    }

    private void OnCoyoteEnd()
    {
        _isCoyoteActive = false;
    }
}
