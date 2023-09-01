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
        }
        
        public override void ExitState()
        {
    
        }
        
        public override void CheckSwitchStates()
        {
            //Se o player apertar o botao de pulo, ele dever ir para o estado pulo!
            if (Ctx.IsJumpPressed && !Ctx.RequiresNewJumpPress)
            {
                SwitchState(Factory.Jump());
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
