using Player.StateMachine;
using Player.StateMachine.Alien;
using UnityEngine;

public abstract class AlienBaseState
{
    private bool _isRootState;
    private AlienBaseState _currentSubState;
    private AlienBaseState _currentSuperState;


    protected bool IsRootState
    {
        set => _isRootState = value;
    }
    
    protected AlienStateMachine Ctx { get; }
    
    protected  AlienStateFactory Factory { get; }

    protected AlienBaseState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
    {
        Ctx = currentContext;
        Factory = alienStateFactory;
    }

    public abstract void EnterState();
    protected abstract void UpdateState();
    protected abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();
    protected abstract void PhysicsUpdateState();

    public void UpdateStates()
    {
        UpdateState();
        _currentSubState?.UpdateStates();
    }
    public void PhysicsUpdateStates()
    {
        PhysicsUpdateState();
        _currentSubState?.PhysicsUpdateState();
    }

    /*public void ExitStates()
    {
        ExitState();
        _currentSubState?.ExitStates();
    }*/

    protected void SwitchState(AlienBaseState newState)
    {
        Debug.Log(newState.ToString());
        ExitState();
        
        newState.EnterState();

        if (_isRootState)
        {
            newState._currentSubState = _currentSubState; // mantém o subState
            _currentSubState.SetSuperState(newState); // faz do novo estado o superState do sub atual
            Ctx.CurrentState = newState;
        }
        else
        {
            _currentSuperState?.SetSubState(newState);
        }
    }

    private void SetSuperState(AlienBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
        
    protected void SetSubState(AlienBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
