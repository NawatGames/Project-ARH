using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class AlienEatTest : MonoBehaviour
{
    public GameObject AlienNeck;
    public GameObject AlienHead;
    public Animator AlienAnimator;
    public SpriteRenderer AlienRenderer;
    public bool IsEating;

    void Awake()
    {
        IsEating = false;
        AlienAnimator = gameObject.GetComponent<Animator>();
        AlienRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(IsEating == false)
            {
                IsEating = true;
                AlienAnimator.SetBool("StartedEating", true);
                AlienNeck.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(EatStartCountdown());
            }
        }
    }

    IEnumerator EatStartCountdown()
    {
        yield return new WaitForSeconds(0.73f);
        AlienHead.GetComponent<SpriteRenderer>().enabled = true;
    }
}
