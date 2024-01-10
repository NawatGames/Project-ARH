using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFreezer : MonoBehaviour
{
    private Rigidbody2D alienRb;

    void Awake()
    {
        alienRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FreezeAlien());
        }
    }

    IEnumerator FreezeAlien()
    {
        alienRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(2.73f);
        alienRb.constraints = RigidbodyConstraints2D.None;
    }
}
