using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Coyote : MonoBehaviour
{
    [SerializeField] private float _coyoteTime = 0.2f; // testar se esse valor default está bom

    private bool jumpedAndNotGrounded = false; // Evita que o CoyoteTimer seja chamado quando o player pula

    public UnityEvent CoyoteOrGroundStartEvent; // quando o player ficar grounded
    public UnityEvent CoyoteOrGroundEndEvent; // quando o player não estiver mais grounded, nem dentro do coyoteTimer

    [SerializeField] private IsGrounded _isGroundedComponent;
    [SerializeField] private JumpRequester _jumpRequester;

    private void OnEnable()
    {
        _isGroundedComponent._onGrounded.AddListener(OnGrounded);
        _isGroundedComponent._onNotGrounded.AddListener(OnNotGrounded);
        _jumpRequester.PerformJumpEvent.AddListener(OnJumpPerformed);
    }

    private void OnDisable()
    {
        _isGroundedComponent._onGrounded.RemoveListener(OnGrounded);
        _isGroundedComponent._onNotGrounded.RemoveListener(OnNotGrounded);
        _jumpRequester.PerformJumpEvent.RemoveListener(OnJumpPerformed);
    }

    private IEnumerator CoyoteTimer(float timer)
    {
        //print("Coyote Start");
        yield return new WaitForSeconds(timer);
        //print("Coyote End");
        CoyoteOrGroundEndEvent.Invoke();
    }

    private void OnGrounded()
    {
        jumpedAndNotGrounded = false;
        CoyoteOrGroundStartEvent.Invoke();
        StopAllCoroutines(); // Se não vários CoyotesTimers poderiam coexistir (e o timer não deve rodar/acabar enquanto o player estiver no chao)
    }

    private void OnNotGrounded()
    {
        if(!jumpedAndNotGrounded) // Assim so executa quando o player cai de uma plataforma (sem pular)
        {
            StartCoroutine(CoyoteTimer(_coyoteTime));
        }
    }

    private void OnJumpPerformed()
    {
        CoyoteOrGroundEndEvent.Invoke(); // Não precisa de timer o player não esta mais grounded nem em coyote
        jumpedAndNotGrounded = true;
    }
}
