using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDescendingState : PlayerBaseState
{
    private IsGrounded _isGrounded;
    private JumpRequester _jumpRequester;

    public PlayerDescendingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, IsGrounded isGrounded, JumpRequester jumpRequester) : base(
        currentContext, playerStateFactory)
    {
        _isGrounded = isGrounded;
        _jumpRequester = jumpRequester;

        _isRootState = true;
    }

    public override void EnterState()
    {
        //Debug.Log("--> descending state");

        _isGrounded.ToGroundedStateChangeEvent.AddListener(GoToGroundedState);
        _jumpRequester.PerformJumpEvent.AddListener(GoToAscendingState);
    }

    public override void ExitState()
    {
        _isGrounded.ToGroundedStateChangeEvent.RemoveListener(GoToGroundedState);
        _jumpRequester.PerformJumpEvent.RemoveListener(GoToAscendingState);
    }

    public override void FixedUpdateState()
    {
        _ctx.RB.velocity += Physics2D.gravity.y * (_ctx.Data.fallGravityMult - 1) * Time.fixedDeltaTime * Vector2.up;
    }

    public void GoToGroundedState() // Ao cair no chão
    {
        SwitchStates(_factory.Grounded());
    }

    public void GoToAscendingState() // Ao pular no ar (coyote ou double jump)
    {
        SwitchStates(_factory.Ascending());
    }
}
