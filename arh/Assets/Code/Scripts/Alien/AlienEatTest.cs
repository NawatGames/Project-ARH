using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.Events;

public class AlienEatTest : MonoBehaviour
{
    public GameObject alienNeck;
    public GameObject alienHead;
    public Animator alienAnimator;
    public SpriteRenderer alienRenderer;
    public bool isEating;
    public bool isMoving;
    public float headMoveTime;
    public float foodSize;
    public float headMoveDistance;
    public UnityEvent finishedEatingEvent;
    private Vector2 originalNeckScale;
    private Vector3 originalHeadPos;

    void Awake()
    {
        isEating = false;
        isMoving = false;
        alienAnimator = gameObject.GetComponent<Animator>();
        alienRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalNeckScale = alienNeck.transform.localScale;
        headMoveDistance = foodSize/2;
    }

    void Update()
    {
        headMoveDistance = foodSize/2;

        if(Input.GetKeyDown(KeyCode.I) | Input.GetKeyDown(KeyCode.J) | Input.GetKeyDown(KeyCode.K) | Input.GetKeyDown(KeyCode.L))
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }

        if(isMoving == false)
        {
            if(Input.GetKeyDown(KeyCode.LeftBracket) & isMoving == false)
            {
                Debug.Log("Comendo");
            }
            
        }
    }

    IEnumerator EatStartCountdown()
    {
        yield return new WaitForSeconds(0.73f);
        alienHead.GetComponent<SpriteRenderer>().enabled = true;
        originalHeadPos = alienHead.transform.position;
        Debug.Log(originalHeadPos.ToString());
        yield return new WaitForSeconds(0.5f);
        alienHead.transform.DOLocalMoveY(headMoveDistance, headMoveTime);
        alienNeck.transform.DOScaleY(foodSize, 0.5f);
        yield return new WaitForSeconds(1.0f);
        alienHead.transform.DOLocalMoveY(-0.0378f, headMoveTime);
        alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        //comentar os dois tween de cima e descomentar o localscale de baixo se quiser que corte direto pro final da ainmação
        yield return new WaitForSeconds(0.5f);
        alienNeck.GetComponent<SpriteRenderer>().enabled = false;
        alienHead.GetComponent<SpriteRenderer>().enabled = false;
        //alienNeck.transform.DOScaleY(originalNeckScale.y, 0.5f);
        alienAnimator.SetBool("FinishedEating", true);
        alienAnimator.SetBool("StartedEating", false);
        finishedEatingEvent.Invoke();
        Debug.Log("Invocou");
    }

    public void AlienEat()
    {
        if(isEating == false)
                {
                    isEating = true;
                    alienAnimator.SetBool("StartedEating", true);
                    alienNeck.GetComponent<SpriteRenderer>().enabled = true;
                    StartCoroutine(EatStartCountdown());
                    isEating = false;
                    alienAnimator.SetBool("FinishedEating", false);
                }
    }
}
