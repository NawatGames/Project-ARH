using DefaultNamespace;

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    // Constructor
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
        return new PlayerWalkingState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpingState(_context, this);
    }

    public PlayerBaseState Apex()
    {
        return new PlayerApexState(_context, this);
    }

    public PlayerBaseState Fall()
    {
        return new PlayerFallingState(_context, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
}