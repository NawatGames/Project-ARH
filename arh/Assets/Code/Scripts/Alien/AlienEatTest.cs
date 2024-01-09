using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

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
            Debug.Log("MOVENDO");
        }

        else
        {
            isMoving = false;
        }

        if(isMoving == false)
        {
            if(Input.GetKeyDown(KeyCode.F) & isMoving == false)
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

        if(isMoving == true)
        {
            Debug.Log("Mov1");
            if(Input.GetKeyDown(KeyCode.F) & isMoving == true)
            {
                Debug.Log("Mov2");
                if(isEating == false)
                {
                    Debug.Log("Mov3");
                    isEating = true;
                    alienAnimator.SetBool("StartedEating2", true);
                    alienNeck.GetComponent<SpriteRenderer>().enabled = true;
                    StartCoroutine(EatStartCountdown2());
                    isEating = false;
                    alienAnimator.SetBool("FinishedEating2", false);
                    alienAnimator.SetBool("FinishedEating3", false);
                }
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
    }

    IEnumerator EatStartCountdown2()
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

        if(isMoving)
        {
            alienAnimator.SetBool("FinishedEating2", true);
        }

        else
        {
            alienAnimator.SetBool("FinishedEating3", true);
        }

        alienAnimator.SetBool("StartedEating2", false);
    }
}
