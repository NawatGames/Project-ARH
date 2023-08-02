using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EatBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _edibleObject;
    [SerializeField] private bool _objectIsInRange = false;
    [SerializeField] private bool _eatingWasPressed = false;
    [SerializeField] private bool _objectWasAte;
    private Color _edibleObjectOriginialColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_objectIsInRange == true)
        {
            _edibleObject.GetComponent<SpriteRenderer>().color = Color.blue;
            if (_eatingWasPressed == true)
            {
                _eatingWasPressed = false;
                _objectWasAte = true;
            }
        }
        else
        {
            _edibleObject = null;
        }

        if (_objectWasAte == true)
        {
            _edibleObject.transform.position = gameObject.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_objectIsInRange == false)
        {
            _edibleObject = other.gameObject;
            _edibleObjectOriginialColor = other.gameObject.GetComponent<SpriteRenderer>().color;
            _objectIsInRange = true;
            Debug.Log("encostei em algo!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _objectIsInRange = false;
        _edibleObject.GetComponent<SpriteRenderer>().color = _edibleObjectOriginialColor;
    }
}