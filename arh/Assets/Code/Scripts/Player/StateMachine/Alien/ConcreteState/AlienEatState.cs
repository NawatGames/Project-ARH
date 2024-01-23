using System;
using System.Collections;
using System.Collections.Generic;
using Player.StateMachine.Alien;
using UnityEngine;

public class AlienEatState : AlienBaseState
{
    public AlienEatState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("Entrou no eat");
        Ctx.alienEatTest.AlienEat();
        Ctx.alienEatTest.finishedEatingEvent.AddListener(GoToGrounded);
        Debug.Log("Go");
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
    }

    protected override void PhysicsUpdateState()
    {
        
    }

    protected override void ExitState()
    {
        Ctx.alienEatTest.finishedEatingEvent.RemoveListener(GoToGrounded);
        Debug.Log("Remove");
    }

    public override void CheckSwitchStates()
    {
        
    }

    public sealed override void InitializeSubState()
    {

    }

    public void GoToGrounded()
    {
        SwitchState(Factory.Grounded());
        Debug.Log("Grounded");
    }
}