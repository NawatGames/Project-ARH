using Player.StateMachine.Alien;
using UnityEngine;

public class AlienCrouchState : AlienBaseState
{
    public AlienCrouchState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) : base(
        currentContext, alienStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Hello from CrouchState");

        Ctx.animationEventsScript.alienStandUpEvent.AddListener(goToGroundedState);

        var size = Ctx.BoxCollider.size;
        var offset = Ctx.BoxCollider.offset;
        size = new Vector2(size.x, size.y - Ctx._crouchSizeReduction);
        offset = new Vector2(offset.x, offset.y - (Ctx._crouchSizeReduction/3));
        Ctx.BoxCollider.size = size;
        Ctx.BoxCollider.offset = offset;
        
        Ctx.ResetJumpCount();

        Ctx.animator.SetTrigger("startShrinking");

    }

    protected override void UpdateState()
    {
        Ctx.ResetCoyoteTime();
        CheckSwitchStates();
    }

    protected override void ExitState()
    {
        var size = Ctx.BoxCollider.size;
        var offset = Ctx.BoxCollider.offset;
        size = new Vector2(size.x, size.y + Ctx._crouchSizeReduction);
        offset = new Vector2(offset.x, offset.y + (Ctx._crouchSizeReduction/3));
        Ctx.BoxCollider.size = size;
        Ctx.BoxCollider.offset = offset;

        Ctx.animationEventsScript.alienStandUpEvent.RemoveListener(goToGroundedState);
    }

    public override void CheckSwitchStates()
    {
        if(!Ctx.LmCollision._isHittingRoof)
        {
            if (!Ctx._isCrouchPressed && !Ctx.IsStandingUp)
            {
                Ctx.IsStandingUp = true;
                Ctx.animator.SetTrigger("startStandingUp"); // Isso autom�ticamente causar� a troca para o estado Grounded (assim q anima��o terminar - evento)
            }
        }

        if(Ctx.IsStandingUp && Ctx.LmCollision._isHittingRoof) // Caso ele saia do teto, comece a levantar e volte pro teto
        {
            Ctx.IsStandingUp = false;
            Ctx.animator.SetTrigger("startIdleShrunk"); // volta � posi��o agachado
        }
        

        if (!Ctx.IsGrounded)
        {
            SwitchState(Factory.Falling());
        }
    }

    private void goToGroundedState() // Fun��o que escuta o evento no fim da anima��o de stand up
    {
        Ctx.IsStandingUp = false;

        SwitchState(Factory.Grounded());
        if(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f && Ctx.Rb.velocity.x < 0.01f)
        {
            Ctx.animator.SetTrigger("startIdle");
        }
        else
        {
            Ctx.animator.SetTrigger("startRunning");
        }
    }

    public override void InitializeSubState()
    {

    }

    protected override void PhysicsUpdateState()
    {
    }
}