using Player.StateMachine;
using UnityEngine;

namespace Code.Scripts.Player.StateMachine.Astronaut.ConcreteStates
{
    public class AstronautIdleState : AstronautBaseState
    {
        public AstronautIdleState(AstronautStateMachine currentContext, AstronautStateFactory astronautStateFactory)
            : base(currentContext, astronautStateFactory){}
        
        public override void EnterState()
        {
            if (Ctx.CurrentState is AstronautGroundedState)
            {
                Ctx.ChangeAnimation("AstronautIdle");
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void UpdateState()
        {
            CheckSwitchStates();
            
            if (Ctx.CurrentState is AstronautGroundedState)
            {
                Ctx.ChangeAnimation("AstronautIdle");
            }
        }

        protected override void PhysicsUpdateState()
        {
            Ctx.Rb.velocity = new Vector2(0f, Ctx.Rb.velocity.y);
        }

        protected override void ExitState()
        {
    
        }
        
        public override void CheckSwitchStates()
        {
            if (!(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f))
            {
                SwitchState(Factory.Walk());
            }
        }
        
        public override void InitializeSubState()
        {
    
        }
    }
}
