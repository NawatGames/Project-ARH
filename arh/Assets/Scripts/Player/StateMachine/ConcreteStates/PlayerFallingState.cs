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
            
            if (Ctx.IsJumpPressed && !Ctx.RequiresNewJumpPress && !Ctx.RequiresNewJumpPress)
            {
                Ctx.CurrentBufferTime = Ctx.BufferTimer;
                Ctx.OnBufferTime = true;
            }
            
            Ctx.CurrentBufferTime = Mathf.Clamp(Ctx.CurrentBufferTime - Time.deltaTime, 0, Ctx.BufferTimer);

            if (Ctx.CurrentBufferTime == 0f)
            {
                Ctx.OnBufferTime = false;
            }
            else
            {
                Ctx.OnBufferTime = true;
            }
        }

        public override void ExitState()
        {
            Ctx.CurrentBufferTime = 0f;
            Ctx.OnBufferTime = false;
        }

        public override void CheckSwitchStates()
        {
            
            if (Ctx.IsJumpPressed && Ctx.CanDoubleJump)
            {
                Ctx.CanDoubleJump = false;
                SwitchState(Factory.Ascend());
            }
            
            if (Ctx.OnBufferTime && Ctx.IsGrounded)
            {
                SwitchState(Factory.Ascend());
            }
            else if (Ctx.IsGrounded && !Ctx.OnBufferTime)
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