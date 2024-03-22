public abstract class AlienBaseState
{
    private bool _isRootState;
    protected AlienBaseState currentSubState;
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
        currentSubState?.UpdateStates();
    }
    public void PhysicsUpdateStates()
    {
        PhysicsUpdateState();
        currentSubState?.PhysicsUpdateState();
    }

    /*public void ExitStates()
    {
        ExitState();
        _currentSubState?.ExitStates();
    }*/

    protected void SwitchState(AlienBaseState newState)
    {
        //Debug.Log(newState.ToString()); // DEBUG ALL
        ExitState();

        if (_isRootState)
        {
            newState.currentSubState = currentSubState; // mantém o subState
            currentSubState.SetSuperState(newState); // faz do novo estado o superState do sub atual
            Ctx.CurrentState = newState;
        }
        else
        {
            _currentSuperState?.SetSubState(newState);
        }

        newState.EnterState();
    }

    private void SetSuperState(AlienBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
        
    protected void SetSubState(AlienBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
