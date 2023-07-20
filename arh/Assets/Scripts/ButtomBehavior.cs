using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtomBehavior : MonoBehaviour
{
    [SerializeField] private GameObject interactable;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool _isInRange = false;
    private bool _isInteracting;
    private bool _isActive;

    public Color notActiveColor;
    public Color activeColor;



    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        if (_isInRange == false && _isInteracting == true && _isActive == false)
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
        if (_isActive == false)
        {
            if (_isInRange == true)
            {
                Debug.Log("Botao ativado!");
                _renderer.color = activeColor;
                _isInteracting = true;
                _isActive = true;

            }
        }
        else
        {
            if (_isInRange == true)
            {
                Debug.Log("Botao Desativado!");
                _renderer.color = notActiveColor;
                _isInteracting = true;
                _isActive = false;
            }
        }
    }
}