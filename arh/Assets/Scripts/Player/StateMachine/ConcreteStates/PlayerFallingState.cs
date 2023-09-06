using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerFallingState : PlayerBaseState
    {
        public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }


        public override void EnterState()
        {
            Debug.Log("HELLO FROM FALLINGSTATE");

        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.Grounded());
            }
            
        }

        public override void InitializeSubState()
        {
            if (!Ctx.IsMovementPressed)
            {
                SetSubState(Factory.Idle());
            }
            else if (Ctx.IsMovementPressed)
            {
                SetSubState(Factory.Walk());
            }
        }

        public override void PhysicsUpdateState()
        {
        }
    }
}