using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    public float platformTimer;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisão");
            StartCoroutine(PlatformCountdown());
        }
    }

    IEnumerator PlatformCountdown()
    {
        yield return new WaitForSeconds(platformTimer);
        PlatformFall();
    }

    void PlatformFall()
    {
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Destroy(gameObject);
        Debug.Log("Caindo");
    }
}
