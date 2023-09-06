using UnityEngine;

namespace Player.StateMachine.ConcreteStates
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory){}
        
        public override void EnterState()
        {
            //Debug.Log("HELLO FROM IDLESTATE");

        }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void PhysicsUpdateState()
        {
            //Debug.Log("Physics Update Called!");
            Ctx.Rigidbody2D.velocity = new Vector2(0f, Ctx.Rigidbody2D.velocity.y);

        }
        public override void ExitState()
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
