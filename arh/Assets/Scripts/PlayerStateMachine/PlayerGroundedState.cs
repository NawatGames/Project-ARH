using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){}

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState(){}

    public override void InitializeSubState()
    {
        if (!_ctx.IsMoving)
        {
            SetSubState(_factory.Idle());
        }
        else SetSubState(_factory.Walk());
    }


    public override void CheckSwitchStates()
    {
        if (_ctx.IsJumping)
        {
            SwitchState(_factory.Jump());
        }
    }
}
