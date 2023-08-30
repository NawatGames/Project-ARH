using Player.StateMachine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine _ctx;
        protected PlayerStateFactory _factory;
        
        public UnityEvent EnterStateEvent;
        public UnityEvent LeaveStateEvent;

        // Constructor
        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            _ctx = currentContext;
            _factory = playerStateFactory;
        }
        
        public virtual void EnterState()
        {
            EnterStateEvent.Invoke();
        }

        public virtual void UpdateState()
        {

        }

        public virtual void ExitState()
        {
            LeaveStateEvent.Invoke();
        }

        public virtual void InitializeSubState()
        {
            
        }

        public virtual void CheckSwitchStates()
        {
            
        }

        void UpdateStates()
        {
            
        }

        protected void SwitchState(PlayerBaseState newState)
        {
            // current state exits state
            ExitState();
            
            // new state enters state
            newState.EnterState();
            
            // switch current state of context
            _ctx.CurrentState = newState;
        }

        protected void SetSuperState()
        {
            
        }

        protected void SetSubState()
        {
            
        }
    }
}