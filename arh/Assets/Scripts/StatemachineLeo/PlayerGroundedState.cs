using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory){}
    public override void EnterState()
    {
        Debug.Log("HELLO FROM GROUNDSTATE");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();

    }
    public override void ExitState()
    {
    
    }
    public override void CheckSwitchStates()
    {
        //Se o player apertar o botao de pulo, ele dever ir para o estado pulo!
        if (_ctx.IsJumpPressed)
        {
            SwitchState(_factory.Jump());
        }
    
    }
    public override void InitializeSubState()
    {
    
    }
}
