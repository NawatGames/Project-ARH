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

        Ctx.animationEventsScript.alienStandUpEvent.AddListener(GoToGroundedState);

        var size = Ctx.CapsuleCollider.size;
        var offset = Ctx.CapsuleCollider.offset;
        size = new Vector2(size.x, size.y - Ctx.CrouchSizeReduction);
        offset = new Vector2(offset.x, offset.y - (Ctx.CrouchSizeReduction/2));
        Ctx.CapsuleCollider.size = size;
        Ctx.CapsuleCollider.offset = offset;
        
        Ctx.ResetJumpCount();
        // Ctx.animator.SetTrigger("startShrinking");
        Ctx.ChangeAnimation("AlienShrinkDown");
    }

    protected override void UpdateState()
    {
        Ctx.ResetCoyoteTime();
        CheckSwitchStates();
    }

    protected override void ExitState()
    {
        var size = Ctx.CapsuleCollider.size;
        var offset = Ctx.CapsuleCollider.offset;
        size = new Vector2(size.x, size.y + Ctx.CrouchSizeReduction);
        offset = new Vector2(offset.x, offset.y + (Ctx.CrouchSizeReduction/2));
        Ctx.CapsuleCollider.size = size;
        Ctx.CapsuleCollider.offset = offset;

        Ctx.animationEventsScript.alienStandUpEvent.RemoveListener(GoToGroundedState);
    }

    public override void CheckSwitchStates()
    {
        if(!Ctx.LmCollision._isHittingRoof)
        {
            if (!Ctx.isCrouchPressed && !Ctx.IsStandingUp)
            {
                Ctx.IsStandingUp = true;
                // Ctx.animator.SetTrigger("startStandingUp");
                Ctx.ChangeAnimation("AlienStandUp");
                // Isso automaticamente causará a troca para o estado Grounded (assim que a animação terminar - evento)
            }
        }

        // Caso ele saia do teto, comece a levantar e volte pro teto
        if(Ctx.IsStandingUp && Ctx.LmCollision._isHittingRoof)
        {
            Ctx.IsStandingUp = false;
            // Ctx.animator.SetTrigger("startIdleShrunk"); // volta à posição agachado
            Ctx.ChangeAnimation("AlienIdleShrunk");
        }
        
        if (!Ctx.IsGrounded)
        {
            SwitchState(Factory.Falling());
        }
    }

    private void GoToGroundedState() // Função que escuta o evento no fim da animação de stand up
    {
        Ctx.IsStandingUp = false;

        SwitchState(Factory.Grounded());
        if(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f && Ctx.Rb.velocity.x < 0.01f)
        {
            // Ctx.animator.SetTrigger("startIdle");
            Ctx.ChangeAnimation("AlienIdle");
        }
        else
        {
            // Ctx.animator.SetTrigger("startRunning");
            Ctx.ChangeAnimation("AlienRun");
        }
    }

    public override void InitializeSubState()
    {

    }

    protected override void PhysicsUpdateState()
    {
        
    }
}