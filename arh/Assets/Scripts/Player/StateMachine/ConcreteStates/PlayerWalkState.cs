using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
        : base(currentContext, playerStateFactory){}
    public override void EnterState()
    {
        Debug.Log("HELLO FROM WALKSTATE");

    }
    public override void UpdateState()
    {
        //Debug.Log("Update Called!");
        CheckSwitchStates();
        Ctx.CurrentMovement = new Vector2(Ctx.CurrentMovementInput.x * Ctx.AppliedMovementSpeed,0f);
    }

    public override void PhysicsUpdateState()
    {
        //Debug.Log("Physics Update Called!");
        Ctx.Rigidbody2D.velocity = new Vector2(Ctx.CurrentMovement.x, Ctx.Rigidbody2D.velocity.y);

    }
    public override void ExitState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState()
    {
    
    }
}
