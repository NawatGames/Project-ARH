namespace DefaultNamespace
{
    public class PlayerJumpingState : PlayerBaseState
    {
        public PlayerJumpingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(
            currentContext, playerStateFactory){}
        
        #region BaseState Functions
        public override void EnterState()
        {
            base.EnterState();
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