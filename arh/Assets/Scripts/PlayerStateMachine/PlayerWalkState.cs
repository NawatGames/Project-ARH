using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){}

    public override void EnterState()
    {
        _ctx.IsMoving = true;
        Walk(_ctx.CurrentDir);
    }

    public override void UpdateState()
    {
        MoveIt();
    }

    public override void ExitState(){}

    public override void InitializeSubState(){}

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsMoving)
        {
            SwitchState(_factory.Idle());
        }
    }
    
    void MoveIt()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        _ctx.CurrentDir = dir;
    }
    private void Walk(Vector2 dir)
    {
        if (!_ctx.CanMove) return;
        else
        {
            _ctx.GetRb.velocity = Vector2.Lerp(_ctx.GetRb.velocity, (new Vector2(dir.x * _ctx.CurrentSpeed, _ctx.GetRb.velocity.y)), _ctx.CurrentWallJumpLerp * Time.deltaTime);
        }
    }
}
