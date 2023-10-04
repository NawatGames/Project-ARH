using UnityEngine;

public class AlienCrouchState : AlienBaseState
{
    
    public AlienCrouchState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("Hello from CrouchState");
        var size = Ctx.BoxCollider.size;
        size = new Vector2(size.x, size.y * Ctx._crouchSizeMultiplier);
        Ctx.BoxCollider.size = size;
        
        Ctx.ResetJumpCount();

    }

    protected override void UpdateState()
    {
        Ctx.ResetCoyoteTime();
        CheckSwitchStates();
    }

    protected override void ExitState()
    {
        var size = Ctx.BoxCollider.size;
        size = new Vector2(size.x, size.y / Ctx._crouchSizeMultiplier);
        Ctx.BoxCollider.size = size;
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx._isCrouchPressed)
        {
            SwitchState(Factory.Grounded());
        }
        else if (!Ctx.IsGrounded)
        {
            SwitchState(Factory.Falling());
        }
    }

    public override void InitializeSubState()
    {
        SetSubState(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f ? Factory.Idle() : Factory.Walk());
    }

    protected override void PhysicsUpdateState()
    {
    }
}
