using UnityEngine;

public class AlienApexState : AlienBaseState
{
    public AlienApexState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {

    }

    // ReSharper disable Unity.PerformanceAnalysis
    protected override void UpdateState()
    {
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
        #region Double Jump
        
        if (Ctx.JumpBufferCounter > 0.01f && Ctx.ExtraJumpsCounter > 0)
        {
            Ctx.ExtraJumpsCounter -= 1;
            SwitchState(Factory.Ascend());
        }
        #endregion
        
        if (Ctx.Rb.velocity.y < -Ctx.JumpApexThreshold)
        {
            SwitchState(Factory.Falling());
        }
        
        if (Ctx.IsGrounded) SwitchState(Factory.Grounded());
    }

    public sealed override void InitializeSubState()
    {
        SetSubState(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f ? Factory.Idle() : Factory.Walk());
    }
}
