using UnityEngine;

public class AlienIdleState : AlienBaseState
{
    public AlienIdleState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory){}
    
    public override void EnterState()
    {
        Debug.Log("IDLE");
        
        if (Ctx.CurrentState is AlienCrouchState && !Ctx.IsStandingUp)
        {
            // Ctx.animator.SetTrigger("startIdleShrunk");
            Ctx.ChangeAnimation("AlienIdleShrunk");
        }
        else if (!Ctx.IsStandingUp && Ctx.CurrentState is not AlienEatState) // Se tiver rodando StandigUp tem q deixar terminar p chegar no evento do fim
        {
            // Ctx.animator.SetTrigger("startIdle");
            Ctx.ChangeAnimation("AlienIdle");
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
        if (!(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f) && !(Ctx.CurrentState is AlienEatState))
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeSubState()
    {
    
    }
}
