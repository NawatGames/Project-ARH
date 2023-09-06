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
            Ctx.ActualCoyoteTime = Ctx.CoyoteTimer;
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
            if (!Ctx.IsGrounded && !Ctx.IsJumpPressed)
            {
                Ctx.ActualCoyoteTime = Mathf.Clamp(Ctx.ActualCoyoteTime - Time.deltaTime, 0f, Ctx.CoyoteTimer);

                if (Ctx.ActualCoyoteTime == 0f)
                {
                    Ctx.OnCoyoteTime = false;
                    Debug.Log("Where out Coyote Time!");
                    // Altera para o FallingState!
                }
                else
                {
                    Ctx.OnCoyoteTime = true;
                    Debug.Log("Where on Coyote Time!");
                }
            }
        }

        public override void PhysicsUpdateState()
        {
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            //Se o player apertar o botao de pulo, ele dever ir para o estado pulo!
            if (Ctx.IsJumpPressed && !Ctx.RequiresNewJumpPress && Ctx.OnCoyoteTime)
            {
                SwitchState(Factory.Ascend());
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