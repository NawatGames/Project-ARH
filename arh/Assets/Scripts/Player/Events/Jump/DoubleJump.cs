using UnityEngine;
using UnityEngine.Events;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] private int _maxDoubleJumps = 1;
    private int _doubleJumpsLeft;

    public UnityEvent PerformDoubleJump;
    public UnityEvent JumpFailedEvent;

    [SerializeField] private JumpSelector jumpSelector;
    [SerializeField] private IsGrounded isGrounded;

    private void OnEnable()
    {
        jumpSelector.DoubleJumpEvent.AddListener(TryDoubleJump);
        isGrounded._onGrounded.AddListener(ResetDoubleJump);
    }

    private void OnDisable()
    {
        jumpSelector.DoubleJumpEvent.RemoveListener(TryDoubleJump);
        isGrounded._onGrounded.RemoveListener(ResetDoubleJump);
    }

    private void TryDoubleJump()
    {
        if (_doubleJumpsLeft > 0)
        {
            _doubleJumpsLeft--;
            PerformDoubleJump.Invoke();
        }
        else // Para chegar aqui, o input não conseguiu executar nem pulo normal, nem duplo
        {
            JumpFailedEvent.Invoke(); // JumpBuffer deve ouvir e começar o BufferTimer
        }
    }

    private void ResetDoubleJump()
    {
        _doubleJumpsLeft = _maxDoubleJumps;
    }
}