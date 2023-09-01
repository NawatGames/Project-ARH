using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected bool _isRootState = false;
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;
    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    
    public abstract void EnterState();

    public abstract void ExitState();


    /*public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }*/

    protected void SwitchStates(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();

        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        }else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
