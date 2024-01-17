using Code.Scripts.Player.StateMachine.Astronaut.ConcreteStates;

namespace Code.Scripts.Player.StateMachine.Astronaut
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

        public AstronautBaseState Jump()
        {
            return new AstronautJumpState(_context, this);
        }
        
        public AstronautBaseState DoubleJump()
        {
            return new AstronautDoubleJumpState(_context, this);
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
