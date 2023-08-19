using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(
            currentContext, playerStateFactory){}

        #region BaseState Functions
        public override void EnterState()
        {
            Debug.Log("Entered Grounded State!");
        }

        public override void UpdateState()
        {
            base.UpdateState(); 
        }

        public override void ExitState()
        {
            base.ExitState();
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
    }
}