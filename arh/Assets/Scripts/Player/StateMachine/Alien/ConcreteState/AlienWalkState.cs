using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienWalkState : AlienBaseState
{
    public AlienWalkState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory) 
            : base(currentContext, alienStateFactory){}
    
        public override void EnterState()
        {

        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void UpdateState()
        {
            if (Ctx.CurrentMovementInput > 0 && !Ctx.IsFacingRight || Ctx.CurrentMovementInput < 0 && Ctx.IsFacingRight)
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
            
            Ctx.Rb.AddForce(movement * Vector2.right);
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
            if (Mathf.Abs(Ctx.Rb.velocity.x) < 0.01f)
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
