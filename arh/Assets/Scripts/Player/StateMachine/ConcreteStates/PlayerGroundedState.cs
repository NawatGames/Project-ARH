using System;
using Player.Movement;
using Unity.Mathematics;
using UnityEngine;

namespace Player.StateMachine.ConcreteStates
{
    public class PlayerGroundedState : PlayerBaseState
    {
        public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void EnterState()
        {
            Debug.Log("HELLO FROM GROUNDSTATE");
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
            if (Ctx.IsGrounded)
            {
                Ctx.CurrentCoyoteTime = Ctx.CoyoteTimer;
                Ctx.OnCoyoteTime = true;
            }

            if (!Ctx.IsGrounded && !Ctx.IsJumpPressed)
            {
                Ctx.CurrentCoyoteTime = Mathf.Clamp(Ctx.CurrentCoyoteTime - Time.deltaTime, 0f, Ctx.CoyoteTimer);

                if (Ctx.CurrentCoyoteTime == 0f)
                {
                    Ctx.OnCoyoteTime = false;
                    //Debug.Log("Where out Coyote Time!");
                }
                else
                {
                    Ctx.OnCoyoteTime = true;
                    //Debug.Log("Where on Coyote Time!");
                }
            }
        }

        public override void PhysicsUpdateState()
        {
        }

        public override void ExitState()
        {
            Ctx.CurrentCoyoteTime = 0f;
            Ctx.OnCoyoteTime = false;
        }

        public override void CheckSwitchStates()
        {
            //Se o player apertar o botao de pulo, ele dever ir para o estado pulo!
            if (Ctx.IsJumpPressed && !Ctx.RequiresNewJumpPress && Ctx.CurrentCoyoteTime > 0)
            {
                SwitchState(Factory.Ascend());
            }

            if (!Ctx.OnCoyoteTime && !Ctx.IsGrounded)
            {
                SwitchState(Factory.Falling());
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
    }
}