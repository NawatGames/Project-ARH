using UnityEngine;
using UnityEngine.Events;

namespace Player.Movement
{
    public class JumpSelector : MonoBehaviour
    {
        [SerializeField] private JumpBuffer jumpBuffer;
        [SerializeField] private CoyoteTime coyoteTime;

        private bool _isCoyoteTimeActive;

        public UnityEvent jumpEvent;
        public UnityEvent doubleJumpEvent;
        public UnityEvent jumpCanceledEvent;

        private void OnEnable()
        {
            jumpBuffer.jumpBufferedChangedEvent.AddListener(OnJumpBufferedChanged);
            coyoteTime.coyoteTimeActiveChangedEvent.AddListener(OnCoyoteTimeActiveChanged);
        }

        private void OnDisable()
        {
            jumpBuffer.jumpBufferedChangedEvent.RemoveListener(OnJumpBufferedChanged);
            coyoteTime.coyoteTimeActiveChangedEvent.RemoveListener(OnCoyoteTimeActiveChanged);
        }

        private void OnJumpBufferedChanged(bool arg0)
        {
            if (arg0) // if jump is buffered
            {
                if (_isCoyoteTimeActive)
                {
                    jumpEvent.Invoke();
//pensar numa maneira para resetar o buffer de pulo quando o player pular de fato, e desativar o coyote, para não permitir multiplos pulos
                }
                else 
                {
                    doubleJumpEvent.Invoke();
//pensar numa maneira para resetar o buffer de pulo quando o player pular de fato, e desativar o coyote, para não permitir multiplos pulos
                }
            }else{
                jumpCanceledEvent.Invoke();
            }
        }

        private void OnCoyoteTimeActiveChanged(bool arg0)
        {
            _isCoyoteTimeActive = arg0;
        }
    }
}