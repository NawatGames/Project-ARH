using UnityEngine;

namespace Player.StateMachine.Astronaut.ConcreteStates
{
    public class AstronautFallingState : AstronautBaseState
    {
        public AstronautFallingState(AstronautStateMachine currentContext, AstronautStateFactory astronautStateFactory)
            : base(currentContext, astronautStateFactory)
        {
            IsRootState = true;
        }
        
        public override void EnterState()
        {
            Debug.Log("ASTRONAUT FALLING");
            
            Ctx.Rb.gravityScale *= Ctx.FallGravityMultiplier;
            Ctx.ChangeAnimation("AstronautFalling");
        }

        protected override void UpdateState()
        {
            CheckSwitchStates();
            
            Ctx.CoyoteTimeCounter = Mathf.Clamp(Ctx.CoyoteTimeCounter - Time.deltaTime, 0f, Ctx.CoyoteTimeCounter);
        }

        protected override void ExitState()
        {
            Ctx.Rb.gravityScale = Ctx.NormalGravityScale;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void CheckSwitchStates()
        {
            if (Ctx.JumpBufferCounter > 0.01f)
            {
                if (Ctx.CoyoteTimeCounter > 0.01f)
                {
                    SwitchState(Factory.Ascend());
                }
                else if (Ctx.ExtraJumpsCounter > 0)
                {
                    // Double Jump
                    Ctx.ExtraJumpsCounter -= 1;
                    SwitchState(Factory.Ascend());
                }
            }
            
            if (Ctx.IsGrounded) SwitchState(Factory.Grounded());
        }

        public sealed override void InitializeSubState()
        {
            
        }

        protected override void PhysicsUpdateState()
        {
            #region Fall Gravity + Clamped Fall Speed
            var vel = Ctx.Rb.velocity;
            Ctx.Rb.gravityScale = Ctx.NormalGravityScale * Ctx.FallGravityMultiplier;
            Ctx.Rb.velocity = new Vector2(vel.x, Mathf.Max(vel.y, -Ctx.MaxFallSpeed));
            #endregion
        }
    }
}