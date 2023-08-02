using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EatBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _edibleObject;
    [SerializeField] private bool _wasTouched = false;
    [SerializeField] private bool _eatingWasPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_wasTouched == true)
        {
            _edibleObject.transform.GetComponent<SpriteRenderer>().color = Color.blue;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _edibleObject = col.gameObject;
        _wasTouched = true;
        //Debug.Log("encostei em algo!");
    }
}
