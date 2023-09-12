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
            //Debug.Log("HELLO FROM ASCENDINGSTATE");
            Ctx.IsJumpPressed = false;
            Ctx.IsJumping = true;
            Ctx.CurrentJumpCutTime = Ctx.PlayerData.JumpCutTimer;
            Ctx.Rb.velocity = new Vector2(Ctx.Rb.velocity.x, 0f); 
            //HandleJump();

        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            

        }
        public override void PhysicsUpdateState()
        {
            if (Ctx.IsJumping)
            {
                if (Ctx.CurrentJumpCutTime >= 0f)
                {
                    HandleJump();
                    Ctx.CurrentJumpCutTime -= Time.deltaTime;
                }
                
            }
        
        }
        public override void ExitState()
        {
            Ctx.CurrentJumpCutTime = 0f;
        }
        public override void CheckSwitchStates()
        {
            if (Ctx.IsJumpPressed && Ctx.CanDoubleJump )
            {
                Debug.Log("DoubleJump");
                Ctx.CanDoubleJump = false;
                SwitchState(Factory.Ascend());

                //HandleJump();
            }
            if (Ctx.Rb.velocity.y < 0)
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

        void HandleJump()
        {   //Debug.Log("JUMP FUNCTION EXECUTED!");
            Ctx.Rb.velocity = new Vector2(Ctx.Rb.velocity.x, Ctx.PlayerData.AppliedJumpForce);
            //Ctx.Rb.AddForce(Vector2.up * Ctx.PlayerData.AppliedJumpForce,ForceMode2D.Impulse);
        }
    }
}
