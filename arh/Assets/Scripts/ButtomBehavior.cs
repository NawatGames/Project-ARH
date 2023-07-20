using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtomBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool _isInRange = false;
    public bool _isActive = false;
    public bool isInteracting = false;

    public Color notActiveColor;
    public Color activeColor;



    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        if (_isInRange == false && _isActive == false)
        {
            _renderer.color = notActiveColor;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
            Debug.Log("Posso apertar o botao!");
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isInRange = false;

        }

    }

    public void ButtonActivation()
    {
        isInteracting = true;
        
        if (_isActive == false)
        {
            if (_isInRange == true)
            {
                _renderer.color = activeColor;
                _isActive = true;
                Debug.Log("Botao ativado!");

            }
        }
        else if(_isActive == true)
        {
            if (_isInRange == true)
            {
                _renderer.color = notActiveColor;
                _isActive = false;
                Debug.Log("Botao Desativado!");
            }
        }
    }
}