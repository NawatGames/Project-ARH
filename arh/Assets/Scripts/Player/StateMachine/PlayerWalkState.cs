using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory): base (currentContext, playerStateFactory){}

    public override void EnterState()
    {
        Debug.Log("--> walk state");
    }

    public override void UpdateState()
    {
	    CheckMovementDirection();
        CheckSwitchState();
    }

    public override void ExitState(){}

    public override void CheckSwitchState()
    {
        if (!_ctx.IsMovementPressed)
        {
            SwitchStates(_factory.Idle());
        }
    }

    public override void InitializeSubState(){}
    
    #region RUN METHODS
    private void Run(float lerpAmount)
	{
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = _ctx.getDir.x;
		//We can reduce are control using Lerp() this smooths changes to are direction and speed
		targetSpeed = Mathf.Lerp(_ctx.getRB.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate
		float accelRate;

		//Gets an acceleration value based on if we are accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		if (_ctx.LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _ctx.Data.runAccelAmount : _ctx.Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _ctx.Data.runAccelAmount * _ctx.Data.accelInAir : _ctx.Data.runDeccelAmount * _ctx.Data.deccelInAir;
		#endregion

		#region Add Bonus Jump Apex Acceleration
		//Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
		if ((_ctx.IsJumping || _ctx.IsJumpFalling) && Mathf.Abs(_ctx.getRB.velocity.y) < _ctx.Data.jumpHangTimeThreshold)
		{
			accelRate *= _ctx.Data.jumpHangAccelerationMult;
			targetSpeed *= _ctx.Data.jumpHangMaxSpeedMult;
		}
		#endregion

		#region Conserve Momentum
		//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
		if(_ctx.Data.doConserveMomentum && Mathf.Abs(_ctx.getRB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(_ctx.getRB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && _ctx.LastOnGroundTime < 0)
		{
			//Prevent any deceleration from happening, or in other words conserve are current momentum
			//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
			accelRate = 0; 
		}
		#endregion

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - _ctx.getRB.velocity.x;
		//Calculate force along x-axis to apply to thr player

		float movement = speedDif * accelRate;

		//Convert this to a vector and apply to rigidbody
		_ctx.getRB.AddForce(movement * Vector2.right, ForceMode2D.Force);

		/*
		 * For those interested here is what AddForce() will do
		 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
	}
    #endregion

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
