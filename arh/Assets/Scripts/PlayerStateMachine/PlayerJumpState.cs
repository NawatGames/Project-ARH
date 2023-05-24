using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
    }

    public override void EnterState()
    {
        Jump(_ctx.CurrentDir);
        BetterJumping();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState(){}

    public override void InitializeSubState(){}

    public override void CheckSwitchStates(){}

    private void Jump(Vector2 dir)
    {
        _ctx.GetRb.velocity = new Vector2(_ctx.GetRb.velocity.x, 0);
        _ctx.GetRb.velocity += dir * _ctx.CurrentJumpForce;
    }
    
    void BetterJumping()
    {
        if(_ctx.GetRb.velocity.y < 0)
        {
            _ctx.GetRb.velocity += Vector2.up * (Physics2D.gravity.y * (_ctx.CurrentFallMult - 1) * Time.deltaTime);
        }else if(_ctx.GetRb.velocity.y > 0 && !Input.GetButton("Jump"))
        { 
            _ctx.GetRb.velocity += Vector2.up * (Physics2D.gravity.y * (_ctx.CurrentLowJumpMult - 1) * Time.deltaTime);
        }
    }
}
