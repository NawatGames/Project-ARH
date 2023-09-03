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

    // Funções usadas por todos estados:

    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void FixedUpdateState(); // Por enquanto somente root states têm (Fixed)Update (não precisa de um updateStateS p incluir subStates)

    /*public void UpdateStates() Por enquanto não tem update
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
        //Debug.Log("--> " + newState);

        if (_isRootState)
        {
            PlayerBaseState subState = _ctx.CurrentState._currentSubState;  // Keep substates
            newState.SetSubState(subState);                                 //
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    // Funções usadas por mais de um estado:

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }


    protected bool CheckForApexZone()
    {
        if (Mathf.Abs(_ctx.RB.velocity.y) < _ctx.Data.apexThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
