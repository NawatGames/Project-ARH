using UnityEngine;
using UnityEngine.Events;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private Transform _groundCheckPosition;
    public UnityEvent _onGrounded;
    public UnityEvent _onNotGrounded;
    public UnityEvent ToGroundedStateChangeEvent;

    private bool _isGrounded;
    private bool _wasGrounded = true;

    private void FixedUpdate()
    {
        _wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPosition.position, _groundCheckRadius, _groundLayer);

        if(_wasGrounded)
        {
            if (!_isGrounded)
            {
                _onNotGrounded.Invoke();
            }
        }
        else
        {
            if (_isGrounded)
            {
                ToGroundedStateChangeEvent.Invoke();    // Tem que chamar esse antes, separado, pra máquina de estados mudar o estado primeiro, antes de outras ações
                _onGrounded.Invoke();                   // (O bufferedJump por exemplo, estava tentando executar antes da troca de estado, assim falhando)
            }                                           // Ficar atento para não acontecer com outros estados
                
        }
    }
}
