using Player.StateMachine.Alien.ConcreteState;

namespace Player.StateMachine.Alien
{
    public class AlienStateFactory
    {
        private readonly AlienStateMachine _context;

        public AlienStateFactory(AlienStateMachine currentContext)
        {
            _context = currentContext;
        }

        public AlienBaseState Idle()
        {
            return new AlienIdleState(_context, this);
        }

        public AlienBaseState Walk()
        {
            return new AlienWalkState(_context, this);
        }

        public AlienBaseState Ascend()
        {
            return new AlienAscendingState(_context, this);
        }

        public AlienBaseState Apex()
        {
            return new AlienApexState(_context, this);
        }
        
        public AlienBaseState Falling()
        {
            return new AlienFallingState(_context, this);
        }

        public AlienBaseState Grounded()
        {
            return new AlienGroundedState(_context, this);
        }

        public AlienBaseState Crouch()
        {
            return new AlienCrouchState(_context, this);
        }

        public AlienBaseState Eat()
        {
            return new AlienEatState(_context, this);
        }
    }
}
