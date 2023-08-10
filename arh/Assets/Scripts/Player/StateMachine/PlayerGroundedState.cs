using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(
        currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {}

    public override void UpdateState()
    {
        CheckSwitchState();
    }
 
    public override void ExitState(){}

    public override void CheckSwitchState()
    {
        if (_ctx.IsJumpPressed)
        {
            SwitchStates(_factory.Jump());
        }
    }

    public override void InitializeSubState()
    {
        if (!_ctx.IsMovementPressed)
        {
            SetSubState(_factory.Idle());
        }else 
        {
            SetSubState(_factory.Walk());
        }
    }
}
