using Player.StateMachine.ConcreteStates;

namespace Player.StateMachine
{
    public class AstronautStateFactory
    {
        private readonly AstronautStateMachine _context;

        public AstronautStateFactory(AstronautStateMachine currentContext)
        {
            _context = currentContext;
        }

        public AstronautBaseState Idle()
        {
            return new AstronautIdleState(_context, this);
        }

        public AstronautBaseState Walk()
        {
            return new AstronautWalkState(_context, this);
        }

        public AstronautBaseState Ascend()
        {
            return new AstrounautAscendingState(_context, this);
        }

        public AstronautBaseState Apex()
        {
            return new AstronautApexState(_context, this);
        }
        
        public AstronautBaseState Falling()
        {
            return new AstronautFallingState(_context, this);
        }

        public AstronautBaseState Grounded()
        {
            return new AstronautGroundedState(_context, this);
        }
    }
}
