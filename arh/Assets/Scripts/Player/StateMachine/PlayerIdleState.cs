using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory): base (currentContext, playerStateFactory){}

    public override void EnterState()
    {
        Debug.Log("--> Idle state");
    }

    /*public override void UpdateState()
    {
        NextState();
    }*/

    public override void ExitState(){}

    public void NextState()
    {
        if (_ctx.IsMovementPressed)
        {
            SwitchStates(_factory.Walk());
        }
    }
}
