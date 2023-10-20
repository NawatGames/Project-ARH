using UnityEngine;

public class AlienGroundedState : AlienBaseState
{
    public AlienGroundedState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.ResetJumpCount();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    protected override void UpdateState()
    {
        Ctx.ResetCoyoteTime();
        CheckSwitchStates();
    }

    protected override void PhysicsUpdateState()
    {
            
    }

    protected override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.JumpBufferCounter > 0.01f) SwitchState(Factory.Ascend());
        else if (!Ctx.IsGrounded) SwitchState(Factory.Falling());
        
        if(Ctx.IsCrouchingPressed) SwitchState((Factory.Crouch()));
    }

    public sealed override void InitializeSubState()
    {
        SetSubState(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f ? Factory.Idle() : Factory.Walk());
    }
}
