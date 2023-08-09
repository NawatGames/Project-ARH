using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienColorChanger : MonoBehaviour
{
    private AlienShrinkingScript alienShrinker;

    void Start()
    {
        alienShrinker = gameObject.GetComponent<AlienShrinkingScript>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))// && alienShrinker.isShrunk == false)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }

        if(Input.GetKeyDown(KeyCode.X))// && alienShrinker.isShrunk == true)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
    }
}
