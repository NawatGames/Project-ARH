using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public abstract class VerticalState : MonoBehaviour
    {
        public UnityEvent EnterStateEvent;
        public UnityEvent LeaveStateEvent;

        public virtual void EnterState()
        {
            EnterStateEvent.Invoke();
        }

        public virtual void LeaveState()
        {
            LeaveStateEvent.Invoke();
        }

        public virtual void UpdateState()
        {

        }
    }


    public class IsJumpingVerticalState : VerticalState
    {
        
    }


    public class IsOnApexVerticalState : VerticalState
    {
        [SerializeField] private ApexModifiers apexModifiers;
        [SerializeField] private VerticalStateMachine verticalStateMachine; 
        [SerializeField] private GroundedHandler groundedHandler;
        
        [SerializeField] private IsFallingVerticalState isFallingVerticalState;
        [SerializeField] private IsGroundedVerticalState isGroundedVerticalState;

        public override void EnterState()
        {
            base.EnterState();
            apexModifiers.ApexChangedEvent.AddListener(OnApexChanged);
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
        }

        public override void LeaveState()
        {
            base.EnterState();
            apexModifiers.ApexChangedEvent.RemoveListener(OnApexChanged);
            groundedHandler.IsGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        private void OnApexChanged(bool arg0)
        {
            if (!arg0)
            {
                verticalStateMachine.SetState(isFallingVerticalState);
            }
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            if (arg0)
            {
                verticalStateMachine.SetState(isGroundedVerticalState);
            }
        }
    }


    public class IsFallingVerticalState : VerticalState
    {
        [SerializeField] private GroundedHandler groundedHandler;
        [SerializeField] private VerticalStateMachine verticalStateMachine;
        [SerializeField] private IsGroundedVerticalState isGrounded;
        
        public override void EnterState()
        {
            base.EnterState();
            groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
            //todo: implementar double jump
        }

        public override void LeaveState()
        {
            base.LeaveState();
            groundedHandler.IsGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        private void OnIsGroundedChanged(bool arg0)
        {
            if (!arg0)
            {
                verticalStateMachine.SetState(isGrounded);
            }
        }
    }
    
    
    public class IsGroundedVerticalState : VerticalState
    {
        
    }
    
    
    public class VerticalStateMachine : MonoBehaviour
    {
        private VerticalState _currentState;
        public UnityEvent<VerticalState> CurrentStateChanged;

        public void SetState(VerticalState verticalState)
        {
            if (_currentState != null)
            {
                _currentState.LeaveState();
            }

            _currentState = verticalState;

            if (_currentState != null)
            {
                _currentState.EnterState();
            }

            CurrentStateChanged.Invoke(_currentState);
        }

        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }
    }
}