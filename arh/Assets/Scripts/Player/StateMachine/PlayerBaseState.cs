namespace Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        private bool _isRootState;
        private PlayerBaseState _currentSubState;
        private PlayerBaseState _currentSuperState;

        // Getters And Setters
        protected bool IsRootState
        {
            set => _isRootState = value;
        }
    
        protected PlayerStateMachine Ctx { get; }

        protected PlayerStateFactory Factory { get; }

        protected PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Ctx = currentContext;
            Factory = playerStateFactory;
        }
    
        public abstract void EnterState();
        protected abstract void UpdateState();
        protected abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();
        protected abstract void PhysicsUpdateState();

        public void UpdateStates()
        {
            UpdateState();
            _currentSubState?.UpdateStates();
        }

        public void PhysicsUpdateStates()
        {
            PhysicsUpdateState();
            _currentSubState?.PhysicsUpdateState();
        }

        public void ExitStates()
        {
            ExitState();
            _currentSubState?.ExitStates();
        }
        
        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
        
            newState.EnterState();

            if (_isRootState)
            {
                Ctx.CurrentState = newState;
            }
            else
            {
                _currentSuperState?.SetSubState(newState);
            }
        }

        private void SetSuperState(PlayerBaseState newSuperState)
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
