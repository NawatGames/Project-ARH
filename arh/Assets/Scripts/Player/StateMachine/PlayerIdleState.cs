using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory): base (currentContext, playerStateFactory){}
    
    public override void EnterState(){}

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState(){}

    public override void CheckSwitchState()
    {
        if (_ctx.IsMovementPressed)
        {
            SwitchStates(_factory.Walk());
        }
    }

    public override void InitializeSubState(){}
}
