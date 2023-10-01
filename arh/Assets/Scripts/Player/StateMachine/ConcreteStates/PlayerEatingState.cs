using System.Collections;
using System.Collections.Generic;
using Player.StateMachine;
using UnityEngine;

public class PlayerEatingState : PlayerBaseState
{
    public PlayerEatingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    protected override void PhysicsUpdateState()
    {
        throw new System.NotImplementedException();
    }
}
