using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public GameObject platformObj;
    public float moveTime;
    public float moveDistance;
    private float backDistance;


    void Start()
    {
        backDistance = platformObj.transform.position.y;
        Debug.Log(backDistance);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.transform.localScale -= new Vector3(0f,0.25f,0f);
        platformObj.transform.transform.DOLocalMoveY(moveDistance, moveTime);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //StartCoroutine(MoveDownCountdown());
        gameObject.transform.localScale += new Vector3(0f,0.25f,0f);
        platformObj.transform.transform.DOLocalMoveY(backDistance, moveTime);
    }

    IEnumerator MoveDownCountdown()
    {
        yield return new WaitForSeconds(2.0f);
    }
}