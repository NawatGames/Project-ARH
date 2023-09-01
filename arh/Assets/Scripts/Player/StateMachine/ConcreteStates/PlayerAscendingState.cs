using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerAscendingState : PlayerBaseState
    {
        public PlayerAscendingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        
        {
            IsRootState = true;
            InitializeSubState();


        }
        public override void EnterState()
        {
            HandleJump();
            Debug.Log("HELLO FROM JUMPSTATE");

        }
        public override void UpdateState()
        {
            CheckSwitchStates();

        }
        public override void ExitState()
        {
            if (Ctx.IsJumpPressed)
            {
                Ctx.RequiresNewJumpPress = true;
            }
        }
        public override void CheckSwitchStates()
        {
            if (Ctx.IsGrounded && Ctx.IsFalling)
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

        void HandleJump()
        {   //Debug.Log("JUMP FUNCTION EXECUTED!");
            Ctx.Rigidbody2D.AddForce(Vector2.up * Ctx.AppliedJumpForce,ForceMode2D.Impulse);
        }
    }
}
