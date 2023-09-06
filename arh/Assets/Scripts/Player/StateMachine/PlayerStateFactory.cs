using Player.StateMachine.ConcreteStates;

namespace Player.StateMachine
{
    public class PlayerStateFactory
    {
        private readonly PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdleState(_context, this);
        }

        public PlayerBaseState Walk()
        {
            return new PlayerWalkState(_context, this);
        }

        public PlayerBaseState Ascend()
        {
            return new PlayerAscendingState(_context, this);
        }

        public PlayerBaseState Falling()
        {
            return new PlayerFallingState(_context, this);
        }

        public PlayerBaseState Grounded()
        {
            return new PlayerGroundedState(_context, this);
        }
    }
}
