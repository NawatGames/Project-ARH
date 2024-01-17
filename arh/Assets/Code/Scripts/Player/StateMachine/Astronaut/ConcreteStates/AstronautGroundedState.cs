using Player.StateMachine;
using UnityEngine;

namespace Code.Scripts.Player.StateMachine.Astronaut.ConcreteStates
{
    public class AstronautGroundedState : AstronautBaseState
    {
        public AstronautGroundedState(AstronautStateMachine currentContext, AstronautStateFactory astronautStateFactory)
            : base(currentContext, astronautStateFactory)
        {
            IsRootState = true;
        }

        public override void EnterState()
        {
            // Debug.Log("ASTRONAUT GROUNDED");
            
            Ctx.ResetJumpCount();
            Ctx.ChangeAnimation("AstronautIdle");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void UpdateState()
        {
            Ctx.ResetCoyoteTime();
            CheckSwitchStates();

            // Ctx.ChangeAnimation(Ctx.Rb.velocity.x != 0 ? "AstronautWalk" : "AstronautIdle");
        }

        protected override void PhysicsUpdateState()
        {

        }

        protected override void ExitState()
        {

        }

        public override void CheckSwitchStates()
        {
            if (Ctx.JumpBufferCounter > 0.01f) SwitchState(Factory.Jump());
            else if (!Ctx.IsGrounded) SwitchState(Factory.Falling());
        }

        public sealed override void InitializeSubState()
        {
            SetSubState(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f ? Factory.Idle() : Factory.Walk());
        }
    }
}