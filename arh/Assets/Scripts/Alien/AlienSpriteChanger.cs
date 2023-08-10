using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpriteChanger : MonoBehaviour
{
    private AlienShrinkingScript alienShrinker;

    void Start()
    {
        alienShrinker = gameObject.GetComponent<AlienShrinkingScript>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)) //&& alienShrinker.isShrunk == false)
        {
            gameObject.GetComponent<SpriteRenderer>().size = new Vector2 (1f, 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.X)) //&& alienShrinker.isShrunk == true)
        {
            gameObject.GetComponent<SpriteRenderer>().size = new Vector2 (1f, 1f);
        }
    }
}
