using System;
using System.Collections;
using System.Collections.Generic;
using Player.StateMachine.Alien;
using UnityEngine;
using DG.Tweening;

public class AlienEatState : AlienBaseState
{
    public SpriteRenderer alienRenderer;
    public bool isEating;
    public bool isMoving;
    private float headMoveDistance;
    private Vector2 originalNeckScale;
    private Vector3 originalHeadPos;

    public AlienEatState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();

        isEating = false;
        alienRenderer = Ctx.spriteObject.GetComponent<SpriteRenderer>();
        originalNeckScale = Ctx.alienNeck.transform.localScale;
        headMoveDistance = Ctx.foodSize / 2;
    }

    public override void EnterState()
    {
        //Debug.Log("Entrou no EatState");

        isEating = true;
        Ctx.animator.SetBool("StartedEating", true);
        Ctx.alienNeck.GetComponent<SpriteRenderer>().enabled = true;
        Ctx.StartCoroutine(EatStartCountdown());
        isEating = false;
        Ctx.animator.SetBool("FinishedEating", false);
    }

    IEnumerator EatStartCountdown()
    {
        yield return new WaitForSeconds(0.73f);
        Ctx.alienHead.GetComponent<SpriteRenderer>().enabled = true;
        originalHeadPos = Ctx.alienHead.transform.position;
        //Debug.Log(originalHeadPos.ToString());
        yield return new WaitForSeconds(0.5f);
        Ctx.alienHead.transform.DOLocalMoveY(headMoveDistance, Ctx.headMoveTime);
        Ctx.alienNeck.transform.DOScaleY(Ctx.foodSize, 0.5f);
        yield return new WaitForSeconds(1.0f);
        Ctx.alienHead.transform.DOLocalMoveY(-0.0378f, Ctx.headMoveTime);
        Ctx.alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        //comentar os dois tween de cima e descomentar o localscale de baixo se quiser que corte direto pro final da ainmação
        yield return new WaitForSeconds(0.5f);
        Ctx.alienNeck.GetComponent<SpriteRenderer>().enabled = false;
        Ctx.alienHead.GetComponent<SpriteRenderer>().enabled = false;
        //alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        Ctx.animator.SetBool("FinishedEating", true);
        Ctx.animator.SetBool("StartedEating", false);
        GoToGrounded();
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
    }
}