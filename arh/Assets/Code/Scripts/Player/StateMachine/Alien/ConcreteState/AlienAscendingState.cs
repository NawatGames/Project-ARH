using Player.StateMachine.Alien;
using UnityEngine;

public class AlienAscendingState : AlienBaseState
{
    public AlienAscendingState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Ctx.jumpCanceledEvent.AddListener(JumpCut);

        Ctx.JumpBufferCounter = 0;
        Ctx.CoyoteTimeCounter = 0;

        Jump();
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
    }

    protected override void PhysicsUpdateState()
    {
    }

    protected override void ExitState()
    {
        Ctx.jumpCanceledEvent.RemoveListener(JumpCut);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void CheckSwitchStates()
    {
        #region Double Jump

        if (Ctx.JumpBufferCounter > 0.01f && Ctx.ExtraJumpsCounter > 0)

        {
            Ctx.ExtraJumpsCounter -= 1;
            SwitchState(Factory.Ascend());
        }

        #endregion

        if (Ctx.Rb.velocity.y < Ctx.JumpApexThreshold)
        {
            SwitchState(Factory.Apex());
        }
    }

    public sealed override void InitializeSubState()
    {
        SetSubState(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f ? Factory.Idle() : Factory.Walk());
    }

    private void Jump()
    {
        Ctx.Rb.velocity = new Vector2(Ctx.Rb.velocity.x, 0f);
        Ctx.Rb.AddForce(Vector2.up * Ctx.JumpForce, ForceMode2D.Impulse);
    }

    private void JumpCut()
    {
        var vel = Ctx.Rb.velocity;
        if (vel.y > 0.01f)
        {
            Ctx.Rb.AddForce(Vector2.down * (vel.y * (1 - Ctx.JumpCutMultiplier)), ForceMode2D.Impulse);
        }
    }
}