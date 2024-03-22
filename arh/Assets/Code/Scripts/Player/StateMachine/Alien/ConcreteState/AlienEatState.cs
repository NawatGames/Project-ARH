using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AlienEatState : AlienBaseState
{
    private SpriteRenderer _alienRenderer;
    private SpriteRenderer _neckSpriteRenderer;
    private SpriteRenderer _headSpriteRenderer;
    private float _headMoveDistance;
    private Vector2 _originalNeckScale;
    private Vector3 _originalHeadPos;

    public AlienEatState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        InitializeSubState();

        _alienRenderer = Ctx.spriteObject.GetComponent<SpriteRenderer>();
        _neckSpriteRenderer = Ctx.alienNeck.GetComponent<SpriteRenderer>();
        _headSpriteRenderer = Ctx.alienHead.GetComponent<SpriteRenderer>();
        _originalNeckScale = Ctx.alienNeck.transform.localScale;
        _headMoveDistance = Ctx.foodSize / 2;
    }

    public override void EnterState()
    {
        Debug.Log("Entrou no EatState");

        Ctx.animationEventsScript.alienAteEvent.AddListener(GoToGrounded);
        Ctx.animationEventsScript.alienEatStartEndEvent.AddListener(StartEatCoroutine);
        // AnimationEvent ao final da animação AlienEatStart vai invocar o evento
        
        Ctx.ChangeAnimation("AlienEatStart");
        _neckSpriteRenderer.enabled = true;

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
        _headSpriteRenderer.enabled = true;
        _originalHeadPos = Ctx.alienHead.transform.position;
        //Debug.Log(originalHeadPos.ToString());
        yield return new WaitForSeconds(0.5f);
        Ctx.alienHead.transform.DOLocalMoveY(_headMoveDistance, Ctx.headMoveTime);
        Ctx.alienNeck.transform.DOScaleY(Ctx.foodSize, 0.5f);
        yield return new WaitForSeconds(1.0f);
        EatObject();
        Ctx.alienHead.transform.DOLocalMoveY(-0.0378f, Ctx.headMoveTime);
        Ctx.alienNeck.transform.DOScaleY(_originalNeckScale.y, 0.5f);
        //comentar os dois tween de cima e descomentar o localscale de baixo se quiser que corte direto pro final da ainmação
        yield return new WaitForSeconds(0.5f);
        _neckSpriteRenderer.enabled = false;
        _headSpriteRenderer.enabled = false;
        //alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        
        Ctx.ChangeAnimation("AlienEatEnd");
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
        Ctx.animationEventsScript.alienEatStartEndEvent.RemoveListener(StartEatCoroutine);
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

    private void StartEatCoroutine()
    { 
        Ctx.StartCoroutine(EatStartCountdown());
        Ctx.ChangeAnimation("AlienEating");
    }
}