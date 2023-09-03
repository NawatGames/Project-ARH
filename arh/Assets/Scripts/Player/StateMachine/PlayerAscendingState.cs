using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAscendingState : PlayerBaseState
{
    private Apogee _apogee;
    private JumpRequester _jumpRequester;

    private bool jumpHasBeenCut;

    public PlayerAscendingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, Apogee apogee, JumpRequester jumpRequester) : base(
        currentContext, playerStateFactory)
    {
        this._apogee = apogee;
        this._jumpRequester = jumpRequester;

        _isRootState = true;
    }

    public override void EnterState()
    {
        jumpHasBeenCut = false;
        //Debug.Log("--> ascending state");

        _apogee.ReachedApogeeEvent.AddListener(GoToDescendingState);
        _jumpRequester.PerformJumpEvent.AddListener(Jump);
        _jumpRequester.ReleasedJumpEvent.AddListener(OnJumpInputReleased);
        Jump();
    }

    public override void ExitState()
    {
        _apogee.ReachedApogeeEvent.RemoveListener(GoToDescendingState);
        _jumpRequester.PerformJumpEvent.RemoveListener(Jump);
        _jumpRequester.ReleasedJumpEvent.RemoveListener(OnJumpInputReleased);
    }

    public override void FixedUpdateState()
    {
        if(jumpHasBeenCut)
        {
            _ctx.RB.velocity += Physics2D.gravity.y * (_ctx.Data.jumpCutGravityMult - 1) * Time.fixedDeltaTime * Vector2.up;
        }
    }

    public void GoToDescendingState()
    {
        SwitchStates(_factory.Descending());
    }

    //public void GoToGroundedState() { } Pra caso o player suba numa plataforma ao mesmo tempo que chega no ápice do pulo (sem descer) - Desnecessário?

    private void Jump()
    {
        var velocity = _ctx.RB.velocity;
        velocity = new Vector2(velocity.x, 0);
        velocity += Vector2.up * _ctx.Data.jumpForce;
        _ctx.RB.velocity = velocity;
        jumpHasBeenCut = false;
    }

    private void OnJumpInputReleased()
    {
        Debug.Log("sadfsdf");
        jumpHasBeenCut = true;
    }

}
