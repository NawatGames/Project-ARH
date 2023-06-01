using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisão");
            //gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject);
            Debug.Log("Caindo");
        }
    }

    //nao funcionou, continuar (ver link)
    private IEnumerator PlatformCountdown()
    {
        int timeCounter = 2;
        int finalTime = 0;

        while(timeCounter > 0)
        {
            yield return new WaitForSeconds(1);
            timeCounter--;
        }

        PlatformFall();
    }

    void PlatformFall()
    {
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Destroy(gameObject);
        Debug.Log("Caindo");
    }
}
