using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory): base (currentContext, playerStateFactory){}

    public override void EnterState()
    {
        Debug.Log("--> walk state");
    }

    /*public override void UpdateState()
    {
	    CheckMovementDirection();
        NextState();
    }*/

    public override void ExitState(){}

    public void NextState()
    {
        if (!_ctx.IsMovementPressed)
        {
            SwitchStates(_factory.Idle());
        }
    }

    private void CheckMovementDirection()
    {
        if (_ctx.getDir.x < 0)
        {
            _ctx.getSide = -1;
            Flip(_ctx.getSide);
        }
        else if (_ctx.getDir.x > 0)
        {
            _ctx.getSide = 1;
            Flip(_ctx.getSide);
        }
    }
    
    private void Flip(int side)
    {
        bool state = (side != 1);
        _ctx.getSR.flipX = state;
    }
}
