using System.Collections;
using System.Collections.Generic;
using Player.StateMachine.Alien;
using UnityEngine;

public class AlienIdleState : AlienBaseState
{
    public AlienIdleState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory){}
        
    public override void EnterState()
    {
        if (Ctx.CurrentState is AlienCrouchState && !Ctx.IsStandingUp)
        {
            Ctx.animator.SetTrigger("startIdleShrunk");
        }
        else if (!Ctx.IsStandingUp && !(Ctx.CurrentState is AlienEatState)) // Se tiver rodando StandigUp tem q deixar terminar p chegar no evento do fim
        {
            Ctx.animator.SetTrigger("startIdle");
        }
    }
        
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void UpdateState()
    {
        CheckSwitchStates();
    }

    protected override void PhysicsUpdateState()
    {
        Ctx.Rb.velocity = new Vector2(0f, Ctx.Rb.velocity.y);
    }

    protected override void ExitState()
    {
    
    }
        
    public override void CheckSwitchStates()
    {
        if (!(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f))
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
