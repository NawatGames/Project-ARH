using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    private InputAction _moveAction;

    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, InputAction moveAction): base (currentContext, playerStateFactory)
    {
        this._moveAction = moveAction;
    }

    public override void EnterState()
    {
        //Debug.Log("--> walk state");

        _moveAction.canceled += NextState;
    }

    public override void ExitState()
    {
        _moveAction.canceled -= NextState;
    }
    public override void FixedUpdateState() { }

    private void NextState(InputAction.CallbackContext context) // escuta o PlayerInput-Move.canceled
    {
        SwitchStates(_factory.Idle());
        _ctx.Dir = 0;
    }
}
