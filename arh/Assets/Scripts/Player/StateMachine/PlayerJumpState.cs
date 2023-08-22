using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(
        currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Jump();
        Debug.Log("--> jump state");
    }

    public override void UpdateState()
    {
        CheckMovementDirection();
        VariableJumpHeight();
        CheckSwitchState();


        if (_ctx.getRB.velocity.y < 0)
        {
            _ctx.IsJumpReleased = true;
        }
    }

    public override void ExitState(){}

    public override void CheckSwitchState()
    {
        if (_ctx.getColl.onGround)
        {
             SwitchStates(_factory.Grounded());
        }
    }

    public override void InitializeSubState(){}
    
    private void Jump()
    {
        var velocity = _ctx.getRB.velocity;
        velocity = new Vector2(velocity.x, 0);
        velocity += Vector2.up * _ctx.getJumpForce;
        _ctx.getRB.velocity = velocity;
    }

    private void VariableJumpHeight()
    {
        if(_ctx.getRB.velocity.y < 0)
        {
            _ctx.getRB.velocity += (Vector2.up * (Physics2D.gravity.y * (_ctx.getFallMultiplier - 1) * Time.deltaTime));
        }else if(_ctx.getRB.velocity.y > 0 && _ctx.IsJumpReleased)
        {
            _ctx.getRB.velocity += Vector2.up * (Physics2D.gravity.y * (_ctx.getLowJumpMultiplier - 1) * Time.deltaTime);
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
