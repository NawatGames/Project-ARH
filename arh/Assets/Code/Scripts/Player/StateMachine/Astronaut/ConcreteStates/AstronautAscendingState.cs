using UnityEngine;

namespace Player.StateMachine.Astronaut.ConcreteStates
{
    public class AstronautAscendingState : AstronautBaseState
    {
        public AstronautAscendingState(AstronautStateMachine currentContext, AstronautStateFactory astronautStateFactory)
            : base(currentContext, astronautStateFactory)
        {
            IsRootState = true;
        }
        
        public override void EnterState()
        {
            Debug.Log("ASTRONAUT ASCENDING");
            
            Ctx.jumpCanceledEvent.AddListener(JumpCut);
            
            Ctx.JumpBufferCounter = 0;
            Ctx.CoyoteTimeCounter = 0;
            
            Jump();
            
            Ctx.ChangeAnimation("AstronautAscending");
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
            if (Ctx.JumpBufferCounter > 0.01f && Ctx.ExtraJumpsCounter > 0)
            {
                // Double Jump
                Ctx.ExtraJumpsCounter -= 1;
                SwitchState(Factory.Ascend());
            }
            
            if (Ctx.Rb.velocity.y < 0)
            {
                SwitchState(Factory.Falling());
            }
        }
        
        public sealed override void InitializeSubState()
        {
            
        }

        private void Jump()
        {
            Ctx.Rb.velocity = new Vector2(Ctx.Rb.velocity.x, 0f);
            Ctx.Rb.AddForce(Vector2.up * Ctx.JumpForce,ForceMode2D.Impulse);
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
}
