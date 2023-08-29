using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory){}
    public override void EnterState()
    {
        HandleJump();
        Debug.Log("HELLO FROM JUMPSTATE");

    }
    public override void UpdateState()
    {
        CheckSwitchStates();

    }
    public override void ExitState()
    {
        if (_ctx.IsJumpPressed)
        {
            _ctx.RequiresNewJumpPress = true;
        }
    }
    public override void CheckSwitchStates()
    {
        if (_ctx.IsGrounded && _ctx.IsFalling)
        {
            SwitchState(_factory.Grounded());
        }
    }
    public override void InitializeSubState()
    {
    
    }

    void HandleJump()
    {   
        _ctx.Rigidbody2D.AddForce(Vector2.up * _ctx.AppliedJumpForce,ForceMode2D.Impulse);
    }
}
