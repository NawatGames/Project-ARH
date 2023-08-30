using Player.StateMachine;
using Player.StateMachine.ConcreteStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerApexState : PlayerBaseState
    {
        [SerializeField] private ApexModifiers apexModifiers;
        [SerializeField] private PlayerStateMachine _playerStateMachine; 
        [SerializeField] private GroundedHandler groundedHandler;
        
        [SerializeField] private PlayerFallingState playerFallingState;
        [SerializeField] private PlayerGroundedState playerGroundedState;
        
        public PlayerApexState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(
            currentContext, playerStateFactory){}

        #region BaseState Functions
        public override void EnterState()
        {
            base.EnterState();
            apexModifiers.ApexChangedEvent.AddListener(OnApexChanged);
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
        
        public override void ExitState()
        {
            base.EnterState();
            apexModifiers.ApexChangedEvent.RemoveListener(OnApexChanged);
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

        private void OnApexChanged(bool arg0)
        {
            if (!arg0)
            {
                _playerStateMachine.SetState(playerFallingState);
            }
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            if (arg0)
            {
                _playerStateMachine.SetState(playerGroundedState);
            }
        }
    }
}