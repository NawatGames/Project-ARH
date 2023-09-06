using System;
using UnityEngine;

namespace Player.StateMachine.ConcreteStates
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
            : base(currentContext, playerStateFactory){}
    
        public override void EnterState()
        {
            //Debug.Log("HELLO FROM WALKSTATE");
        }
    
        public override void UpdateState()
        {
            //Debug.Log("Update Called!");
            CheckSwitchStates();
        }

        public override void PhysicsUpdateState()
        {
            var vel = Ctx.Rigidbody2D.velocity;
            
            #region Walk
            
            var targetSpeed = Ctx.CurrentMovementInput * Ctx.MoveSpeed;
            var speedDif = targetSpeed - vel.x;
            var accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Ctx.Acceleration : Ctx.Deceleration;
            var movement = Mathf.Pow(MathF.Abs(speedDif) * accelRate, Ctx.VelocityPower) * Mathf.Sign(speedDif);
            
            Ctx.Rigidbody2D.AddForce(movement * Vector2.right);
            #endregion
            
            
            #region Friction
            
            if (Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f)
            {
                var amount = Mathf.Min(Mathf.Abs(vel.x), Mathf.Abs(Ctx.FrictionAmount));
                amount *= Mathf.Sign(vel.x);
                Ctx.Rigidbody2D.AddForce(Vector2.right * -amount, ForceMode2D.Impulse); 
            }
            #endregion
        }
        
        public override void ExitState()
        {
        
        }
    
        public override void CheckSwitchStates()
        {
            if (Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f)
            {
                SwitchState(Factory.Idle());
            }
        }
    
        public override void InitializeSubState()
        {
    
        }
    }
}
