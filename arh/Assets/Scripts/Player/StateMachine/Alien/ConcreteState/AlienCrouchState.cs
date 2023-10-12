using UnityEngine;

public class AlienCrouchState : AlienBaseState
{
    public AlienCrouchState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(
        currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Hello from CrouchState");
        
        Ctx.EatContext.AlienCanEat = false;

        var offset = Ctx.BoxCollider.offset;

        var size = Ctx.BoxCollider.size;
        size = new Vector2(size.x, size.y - Ctx._crouchSizeReduction);
        Ctx.BoxCollider.size = size;

        offset = new Vector2(offset.x, offset.y - (Ctx._crouchSizeReduction / 2));
        Ctx.BoxCollider.offset = offset;


        Ctx.ResetJumpCount();
    }

    protected override void UpdateState()
    {
        Ctx.ResetCoyoteTime();
        CheckSwitchStates();
    }

    protected override void ExitState()
    {

        var offset = Ctx.BoxCollider.offset;

        var size = Ctx.BoxCollider.size;
        size = new Vector2(size.x, size.y + Ctx._crouchSizeReduction);
        Ctx.BoxCollider.size = size;

        offset = new Vector2(offset.x, offset.y + (Ctx._crouchSizeReduction / 2));
        Ctx.BoxCollider.offset = offset;
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.lmCollision._isHittingRoof)
        {
            if (!Ctx._isCrouchPressed)
            {
                SwitchState(Factory.Grounded());
            }
        }

        if (!Ctx.IsGrounded)
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