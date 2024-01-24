using Player.StateMachine.Alien;
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
        Ctx.isInteractingEvent.AddListener(GoToEatState);
        //Debug.Log("Listener");
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

    public void GoToEatState()
    {
        SwitchState((Factory.Eat()));
        //Debug.Log("Go");
    }

    protected override void ExitState()
    {
        Ctx.isInteractingEvent.RemoveListener(GoToEatState);
        //Debug.Log("Remove");
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
