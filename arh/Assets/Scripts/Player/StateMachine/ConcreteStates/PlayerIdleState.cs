using UnityEngine;

namespace Player.StateMachine.ConcreteStates
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory){}
        
        public override void EnterState()
        {

        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void UpdateState()
        {
            CheckSwitchStates();
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
