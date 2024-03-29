using System;
using System.Collections;
using System.Collections.Generic;
using Player.StateMachine;
using Player.StateMachine.Astronaut;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtomBehavior : MonoBehaviour
{
    [SerializeField] private AstronautStateMachine _astronautStateMachine;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool _isInRange = false;
    [SerializeField] bool _isActive = false;

    public Color notActiveColor;
    public Color activeColor;

    public UnityEvent isButtonActive;
    public UnityEvent isButtonNotActive;


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _playerGameObject = GameObject.FindWithTag("Astronaut");
        _astronautStateMachine = _playerGameObject.GetComponent<AstronautStateMachine>();
    }


    private void FixedUpdate()
    {
        if (_isInRange == false && _isActive == false)
        {
            _renderer.color = notActiveColor;
        }


    }

    private void OnEnable()
    {
       _astronautStateMachine.isInteractingEvent.AddListener(ButtonActivation);
    }

    private void OnDisable()
    {
        _astronautStateMachine.isInteractingEvent.RemoveListener(ButtonActivation);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Astronaut"))
        {
            _isInRange = true;
            //Debug.Log("Posso apertar o botao!");
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Astronaut"))
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
                _renderer.color = activeColor;
                _isActive = true;
                //Debug.Log("Botao ativado!");
                isButtonActive.Invoke();

            }
        }
        else if(_isActive == true)
        {
            if (_isInRange == true)
            {
                _renderer.color = notActiveColor;
                _isActive = false;
                //Debug.Log("Botao Desativado!");
                isButtonNotActive.Invoke();
            }
        }
    }
}