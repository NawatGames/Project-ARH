using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    private PlayerIdleState _idleState;
    private PlayerWalkState _walkState;
    private PlayerGroundedState _groundedState;
    private PlayerAscendingState _ascendingState;
    private PlayerDescendingState _descendingState;

    public PlayerStateFactory(PlayerStateMachine currentContext, IsGrounded isGround, JumpRequester jumpRequester, Apogee apogee)
    {
        _context = currentContext;

        _idleState = new PlayerIdleState(_context, this);   // Os subStates devem ser criados antes dos superStates !!!
        _walkState = new PlayerWalkState(_context, this);   //
        _groundedState = new PlayerGroundedState(_context, this, jumpRequester, isGround);
        
        _ascendingState = new PlayerAscendingState(_context, this, apogee);
        _descendingState = new PlayerDescendingState(_context, this, isGround);

    }

    public PlayerBaseState Idle()
    {
        return _idleState;
    }

    public PlayerBaseState Walk()
    {
        return _walkState;
    }

    public PlayerBaseState Grounded()
    {
        return _groundedState;
    }
    
    public PlayerBaseState Ascending()
    {
        return _ascendingState;
    }

    public PlayerDescendingState Descending()
    {
        return _descendingState;
    }
}
