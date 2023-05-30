using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    private float platformTimer = 0.0f;

    void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisão");
            if(platformTimer == 2.0f)
            {
                gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                platformTimer = 0.0f;
                Debug.Log("Caindo");
            }
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Caindo");
        }
    }

    void Update()
    {
        platformTimer += Time.deltaTime;
    }    
}
