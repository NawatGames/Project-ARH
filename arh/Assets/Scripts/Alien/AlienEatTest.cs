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
    public float headMoveTime;
    public float foodSize;
    public float headMoveDistance;
    private Vector2 originalNeckScale;
    private Vector3 originalHeadPos;

    void Awake()
    {
        isEating = false;
        alienAnimator = gameObject.GetComponent<Animator>();
        alienRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalNeckScale = alienNeck.transform.localScale;
        headMoveDistance = foodSize/2;
    }

    void Update()
    {
        headMoveDistance = foodSize/2;

        if(Input.GetKeyDown(KeyCode.F))
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
        //alienHead.transform.DOLocalMoveY(originalHeadPos.y, headMoveTime);
        alienHead.transform.DOLocalMoveY(-0.0378f, headMoveTime);
        alienNeck.transform.localScale = originalNeckScale;
        alienNeck.GetComponent<SpriteRenderer>().enabled = false;
        alienHead.GetComponent<SpriteRenderer>().enabled = false;
        alienAnimator.SetBool("FinishedEating", true);
        alienAnimator.SetBool("StartedEating", false);
    }
}
