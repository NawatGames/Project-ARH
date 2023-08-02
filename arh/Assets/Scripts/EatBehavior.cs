using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EatBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _edibleObject;
    [SerializeField] private bool _wasTouched = false;
    [SerializeField] private bool _eatingWasPressed = false;
    private Color _edibleObjectOriginialColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_wasTouched == true)
        {
            _edibleObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            _edibleObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_wasTouched == false)
        {
            _edibleObject = other.gameObject;
            _edibleObjectOriginialColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            _wasTouched = true;
            Debug.Log("encostei em algo!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _wasTouched = false;
        _edibleObject.GetComponent<SpriteRenderer>().color = _edibleObjectOriginialColor;
    }
}