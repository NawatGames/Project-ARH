using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    private PlayerIdleState _idleState;
    private PlayerWalkState _walkState;
    private PlayerGroundedState _groundedState;
    private PlayerAscendingState _ascendingState;
    private PlayerDescendingState _descendingState;

    public PlayerStateFactory(PlayerStateMachine currentContext, PlayerInput playerInput, IsGrounded isGround, JumpRequester jumpRequester, Apogee apogee)
    {
        _context = currentContext;

        InputAction moveAction = playerInput.FindAction("Move");

        _walkState = new PlayerWalkState(_context, this, moveAction);   //
        _idleState = new PlayerIdleState(_context, this, moveAction);   // Os subStates devem ser criados antes dos superStates !!!
        _groundedState = new PlayerGroundedState(_context, this, jumpRequester, isGround);
        
        _ascendingState = new PlayerAscendingState(_context, this, apogee, jumpRequester);
        _descendingState = new PlayerDescendingState(_context, this, isGround, jumpRequester);

    }

    public PlayerIdleState Idle()
    {
        return _idleState;
    }

    public PlayerWalkState Walk()
    {
        return _walkState;
    }

    public PlayerGroundedState Grounded()
    {
        return _groundedState;
    }
    
    public PlayerAscendingState Ascending()
    {
        return _ascendingState;
    }

    public PlayerDescendingState Descending()
    {
        return _descendingState;
    }
}
