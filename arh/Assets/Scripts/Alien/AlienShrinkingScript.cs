using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShrinkingScript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        }
    }
}
