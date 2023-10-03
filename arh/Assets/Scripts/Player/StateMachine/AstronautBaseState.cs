namespace Player.StateMachine
{
    public abstract class AstronautBaseState
    {
        private bool _isRootState;
        private AstronautBaseState _currentSubState;
        private AstronautBaseState _currentSuperState;

        // Getters And Setters
        protected bool IsRootState
        {
            set => _isRootState = value;
        }
    
        protected AstronautStateMachine Ctx { get; }

        protected AstronautStateFactory Factory { get; }

        protected AstronautBaseState(AstronautStateMachine currentContext, AstronautStateFactory astronautStateFactory)
        {
            Ctx = currentContext;
            Factory = astronautStateFactory;
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
        
        protected void SwitchState(AstronautBaseState newState)
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

        private void SetSuperState(AstronautBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        
        protected void SetSubState(AstronautBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}
