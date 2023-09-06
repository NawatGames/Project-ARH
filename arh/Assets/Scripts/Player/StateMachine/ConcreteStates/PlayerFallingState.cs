using UnityEngine;

namespace Player.StateMachine.ConcreteStates
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

            if (Ctx.IsJumpPressed && !Ctx.RequiresNewJumpPress) //&& !Ctx.RequiresNewJumpPress
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
            SetSubState(Mathf.Abs(Ctx.CurrentMovementInput) < 0.01f ? Factory.Idle() : Factory.Walk());
        }

        public override void PhysicsUpdateState()
        {
            var vel = Ctx.Rigidbody2D.velocity;
            var normalGravityScale = Ctx.Rigidbody2D.gravityScale;
            
            #region Fall Gravity + Clamped Fall Speed
            Ctx.Rigidbody2D.gravityScale = normalGravityScale * Ctx.FallGravityMultiplier;
            Ctx.Rigidbody2D.velocity = new Vector2(vel.x, Mathf.Max(vel.y, -Ctx.MaxFallSpeed));
            #endregion
        }
    }
}