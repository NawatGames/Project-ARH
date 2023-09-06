namespace Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        private bool _isRootState = false;
        private PlayerBaseState _currentSubState;
        private PlayerBaseState _currentSuperState;

        // Getters And Setters
        protected bool IsRootState
        {
            set => _isRootState = value;
        }
    
        protected PlayerStateMachine Ctx { get; }

        protected PlayerStateFactory Factory { get; }

        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Ctx = currentContext;
            Factory = playerStateFactory;
        }
    
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        public abstract void PhysicsUpdateState();

        public void UpdateStates()
        {
            UpdateState();
            if (_currentSubState != null)
            {
                _currentSubState.UpdateStates();
            }
        }

        public void PhysicsUpdateStates()
        {
            PhysicsUpdateState();
            if (_currentSubState != null)
            {
                _currentSubState.PhysicsUpdateState();
            }
        }

        public void ExitStates()
        {
            ExitState();
            if (_currentSubState != null)
            {
                _currentSubState.ExitStates();
            }
        }
        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
        
            newState.EnterState();

            if (_isRootState)
            {
                Ctx.CurrentState = newState;
            }
            else if (_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
            }
        }
        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        protected void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    

    }
}
