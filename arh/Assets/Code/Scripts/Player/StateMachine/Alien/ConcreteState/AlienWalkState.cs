using System;
using UnityEngine;

public class AlienWalkState : AlienBaseState
{
    public AlienWalkState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) 
        : base(currentContext, alienStateFactory){}

    public override void EnterState()
    {
        if(Ctx.CurrentState is AlienCrouchState && !Ctx.IsStandingUp)
        {
            // Ctx.animator.SetTrigger("startRunShrunk");
            Ctx.ChangeAnimation("AlienIdleShrunk");
        }
        else if(!Ctx.IsStandingUp && Ctx.CurrentState is not AlienEatState) // Se tiver rodando StandigUp tem q deixar terminar p chegar no evento do fim
        {
            // Ctx.animator.SetTrigger("startRunning");
            Ctx.ChangeAnimation("AlienRun");
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    protected override void UpdateState()
    {
        if (Ctx.CurrentMovementInput > 0 && !Ctx.IsFacingRight || Ctx.CurrentMovementInput < 0 && Ctx.IsFacingRight && !(Ctx.CurrentState is AlienEatState))
        {
            Flip();
        }
        CheckSwitchStates();
    }

    protected override void PhysicsUpdateState()
    {
        var vel = Ctx.Rb.velocity;
        
        #region Walk
        
        var targetSpeed = Ctx.CurrentMovementInput * Ctx.MoveSpeed;
        var speedDif = targetSpeed - vel.x;
        var accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Ctx.Acceleration : Ctx.Deceleration;
        var movement = Mathf.Pow(MathF.Abs(speedDif) * accelRate, Ctx.VelocityPower) * Mathf.Sign(speedDif);
        
        if(!(Ctx.CurrentState is AlienEatState))
        {
            //Debug.Log("Andou");
            Ctx.Rb.AddForce(movement * Vector2.right);
        }
        #endregion
        
        #region Friction
        
        if (Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f)
        {
            var amount = Mathf.Min(Mathf.Abs(vel.x), Mathf.Abs(Ctx.FrictionAmount));
            amount *= Mathf.Sign(vel.x);
            Ctx.Rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse); 
        }
        #endregion
    }

    protected override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Mathf.Abs(Ctx.Rb.velocity.x) < 0.01f && Mathf.Abs(Ctx.CurrentMovementInput) < 0.1)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {

    }
    
    private void Flip()
    {
        var tr = Ctx.Sprite.transform;
        var localScale = tr.localScale;
        tr.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        Ctx.IsFacingRight = !Ctx.IsFacingRight;
    }
}
