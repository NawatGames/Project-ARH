using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDescendingState : PlayerBaseState
{
    private IsGrounded _isGrounded;

    public PlayerDescendingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, IsGrounded isGrounded) : base(
        currentContext, playerStateFactory)
    {
        _isGrounded = isGrounded;
    }

    public override void EnterState()
    {
        _isGrounded.ToGroundedStateChangeEvent.AddListener(NextState);
        Debug.Log("--> descending state");
    }

    public override void ExitState()
    {
        _isGrounded.ToGroundedStateChangeEvent.RemoveListener(NextState);
    }

    public void NextState()
    {
        SwitchStates(_factory.Grounded());
    }
}
