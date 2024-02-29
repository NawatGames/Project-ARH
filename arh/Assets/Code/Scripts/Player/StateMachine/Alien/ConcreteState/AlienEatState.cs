using System;
using System.Collections;
using System.Collections.Generic;
using Player.StateMachine.Alien;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Animations;
using WaitUntil = UnityEngine.WaitUntil;

public class AlienEatState : AlienBaseState
{
    private SpriteRenderer alienRenderer;
    private SpriteRenderer neckSpriteRenderer;
    private SpriteRenderer headSpriteRenderer;
    private float headMoveDistance;
    private Vector2 originalNeckScale;
    private Vector3 originalHeadPos;

    public AlienEatState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();

        alienRenderer = Ctx.spriteObject.GetComponent<SpriteRenderer>();
        neckSpriteRenderer = Ctx.alienNeck.GetComponent<SpriteRenderer>();
        headSpriteRenderer = Ctx.alienHead.GetComponent<SpriteRenderer>();
        originalNeckScale = Ctx.alienNeck.transform.localScale;
        headMoveDistance = Ctx.foodSize / 2;
    }

    public override void EnterState()
    {
        //Debug.Log("Entrou no EatState");

        Ctx.animationEventsScript.alienAteEvent.AddListener(GoToGrounded);
        
        Ctx.animator.SetBool("StartedEating", true);
        neckSpriteRenderer.enabled = true;
        Ctx.StartCoroutine(EatStartCountdown());
        Ctx.animator.SetBool("FinishedEating", false);
        
        // AnimationEvent do fim da animação AlienEatEnd vai chamar o GoToGrounded
    }

    private void EatObject()
    {
        Ctx.hasStoredObject = true;
        Ctx.currentEdibleObject.SetActive(false);
        //Debug.Log("Comi o Objeto");
        // Após o fim
    }

    private IEnumerator EatStartCountdown()
    {
        //yield return new WaitForSeconds(0.73f);    -> Substituido pela linha abaixo (MELHOR TROCAR POR ANIMATION EVENT?)
        yield return new WaitUntil(() => Ctx.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        
        headSpriteRenderer.enabled = true;
        originalHeadPos = Ctx.alienHead.transform.position;
        //Debug.Log(originalHeadPos.ToString());
        yield return new WaitForSeconds(0.5f);
        Ctx.alienHead.transform.DOLocalMoveY(headMoveDistance, Ctx.headMoveTime);
        Ctx.alienNeck.transform.DOScaleY(Ctx.foodSize, 0.5f);
        yield return new WaitForSeconds(1.0f);
        EatObject();
        Ctx.alienHead.transform.DOLocalMoveY(-0.0378f, Ctx.headMoveTime);
        Ctx.alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        //comentar os dois tween de cima e descomentar o localscale de baixo se quiser que corte direto pro final da ainmação
        yield return new WaitForSeconds(0.5f);
        neckSpriteRenderer.enabled = false;
        headSpriteRenderer.enabled = false;
        //alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        Ctx.animator.SetBool("FinishedEating", true);
        Ctx.animator.SetBool("StartedEating", false);
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
        Ctx.animationEventsScript.alienAteEvent.RemoveListener(GoToGrounded);
    }

    public override void CheckSwitchStates()
    {
        
    }

    public sealed override void InitializeSubState()
    {

    }

    private void GoToGrounded() // invocado pelo AnimationEvent do fim da animação AlienEatEnd
    {
        SwitchState(Factory.Grounded());
    }
}