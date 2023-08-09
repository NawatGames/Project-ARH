using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShrinkingScript : MonoBehaviour
{
    public bool isShrunk = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))//&& isShrunk == false)
        {
            //isShrunk = true;
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.X))// && isShrunk == true)
        {
            //isShrunk = false;
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        }
    }
}
