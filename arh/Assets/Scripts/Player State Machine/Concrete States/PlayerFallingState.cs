using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerFallingState : PlayerBaseState
    {
        [SerializeField] private GroundedHandler groundedHandler;
        [SerializeField] private PlayerStateMachine _playerStateMachine;
        [SerializeField] private PlayerGroundedState playerGrounded;
        
        public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(
            currentContext, playerStateFactory){}
        
        #region BaseState Functions
        public override void EnterState()
        {
            base.EnterState();
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
            //todo: implement double jump
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
        
        public override void ExitState()
        {
            base.ExitState();
            groundedHandler.IsGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
        }

        public override void InitializeSubState()
        {
            base.InitializeSubState();
        }

        public override void CheckSwitchStates()
        {
            base.CheckSwitchStates();
        }

        #endregion
        
        private void OnIsGroundedChanged(bool arg0)
        {
            if (!arg0)
            {
                _playerStateMachine.SetState(playerGrounded);
            }
        }
    }
}