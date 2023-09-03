using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    private InputAction _moveAction;

    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, InputAction moveAction): base (currentContext, playerStateFactory)
    {
        this._moveAction = moveAction;
    }

    public override void EnterState()
    {
        //Debug.Log("--> Idle state");
        _moveAction.performed += NextState;
    }

    public override void ExitState()
    {
        _moveAction.performed -= NextState;
    }

    public override void FixedUpdateState() { }

    private void NextState(InputAction.CallbackContext context) // escuta o PlayerInput-Move.performed
    {
        float dir = context.ReadValue<float>();

        if (dir < 0)
        {
            SetFlip(false);
            _ctx.Dir = -1;
        }
        else
        {
            SetFlip(true);
            _ctx.Dir = 1;
        }

        SwitchStates(_factory.Walk());
    }

    private void SetFlip(bool facingRight)
    {
        _ctx.SR.flipX = !facingRight;
    }
}
