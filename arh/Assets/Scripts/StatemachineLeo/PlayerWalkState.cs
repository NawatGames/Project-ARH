using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory){}
    public override void EnterState()
    {
    
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        //_ctx.CurrentMovement.x = 
    }
    public override void ExitState()
    {
    
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState()
    {
    
    }
}
