using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAscendingState : PlayerBaseState
{
    private Apogee _apogee;
    public PlayerAscendingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, Apogee apogee) : base(
        currentContext, playerStateFactory)
    {
        this._apogee = apogee;
    }

    public override void EnterState()
    {
        _apogee.ReachedApogeeEvent.AddListener(NextState);
        Jump();
        Debug.Log("--> ascending state");
    }

    public override void ExitState()
    {
        _apogee.ReachedApogeeEvent.RemoveListener(NextState);
    }

    public void NextState()
    {
        SwitchStates(_factory.Descending());
    }

    private void Jump()
    {
        var velocity = _ctx.getRB.velocity;
        velocity = new Vector2(velocity.x, 0);
        velocity += Vector2.up * _ctx.Data.jumpForce;
        _ctx.getRB.velocity = velocity;
    }

    private void VariableJumpHeight()
    {
        if(_ctx.getRB.velocity.y < 0)
        {
            _ctx.getRB.velocity += (Vector2.up * (Physics2D.gravity.y * (_ctx.Data.fallGravityMult - 1) * Time.deltaTime));
        }else if(_ctx.getRB.velocity.y > 0 && _ctx.IsJumpReleased)
        {
            _ctx.getRB.velocity += Vector2.up * (Physics2D.gravity.y * (_ctx.Data.jumpCutGravityMult - 1) * Time.deltaTime);
        }
    }
    
    
    private void CheckMovementDirection()
    {
        if (_ctx.getDir.x < 0)
        {
            _ctx.getSide = -1;
            Flip(_ctx.getSide);
        }
        else if (_ctx.getDir.x > 0)
        {
            _ctx.getSide = 1;
            Flip(_ctx.getSide);
        }
    }
    
    private void Flip(int side)
    {
        bool state = (side != 1);
        _ctx.getSR.flipX = state;
    }
}
