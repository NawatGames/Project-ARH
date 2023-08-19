using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerStateMachine : MonoBehaviour
    {
        // state variables
        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;
        
        public UnityEvent<PlayerBaseState> CurrentStateChanged;

        // getters and setters
        public PlayerBaseState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        private void Awake()
        {
            // setup state
            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();
        }

        public void SetState(PlayerBaseState playerBaseState)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }

            _currentState = playerBaseState;

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