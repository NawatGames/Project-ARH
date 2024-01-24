using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFreezer : MonoBehaviour
{
    private Rigidbody2D alienRb;
    private RigidbodyConstraints2D originalConstraints;

    void Awake()
    {
        alienRb = GetComponent<Rigidbody2D>();
        originalConstraints = alienRb.constraints;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            StartCoroutine(FreezeAlien());
        }
    }

    IEnumerator FreezeAlien()
    {
        alienRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(2.73f);
        alienRb.constraints = originalConstraints;
    }
}
