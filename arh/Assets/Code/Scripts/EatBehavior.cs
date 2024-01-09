using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EatBehavior : MonoBehaviour
{
    [SerializeField] private AlienStateMachine _alienStateMachine;
    [SerializeField] private GameObject _playerGameObject;

    [SerializeField] private GameObject _edibleObject;

    [SerializeField] private bool _objectIsInRange = false;
    [SerializeField] private bool _objectWasEaten;

    [SerializeField] private float _throwForce;
    private Rigidbody2D _edibleObjectRigidBody;


    private void Awake()
    {
        _playerGameObject = GameObject.FindWithTag("Alien");
        _alienStateMachine = _playerGameObject.GetComponent<AlienStateMachine>();
    }

    public void EatObject()
    {
        if (_objectWasEaten)
        {
            // Cospe o Objeto com uma certa força
            _objectWasEaten = false;
            _edibleObject.SetActive(true);

            if (_alienStateMachine.Sprite.transform.localScale.x <= 0)
            {
                _edibleObjectRigidBody.velocity = new Vector2(_throwForce * -1, _edibleObjectRigidBody.velocity.y);
                
            }
            else
            {
                _edibleObjectRigidBody.velocity = new Vector2(_throwForce, _edibleObjectRigidBody.velocity.y);

            }

            //Debug.Log("Cuspi o Objeto");
        }
        else if (_objectIsInRange && !_objectWasEaten)
        {
            _objectWasEaten = true;
            _edibleObject.SetActive(false);
            _edibleObjectRigidBody = _edibleObject.GetComponent<Rigidbody2D>();
            //Debug.Log("Comi o Objeto");
        }

    }

    void Update()
    {

        if (_objectWasEaten)
        {
            _edibleObject.transform.position = gameObject.transform.position;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_objectWasEaten && !_objectIsInRange && other.CompareTag("Food"))
        {
            _edibleObject = other.gameObject;

            _objectIsInRange = true;
            //Debug.Log("Food Is In Range!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_objectWasEaten)
        {
            _objectIsInRange = false;
            _edibleObject = null;
        }
    }
    private void OnEnable()
    {
        _alienStateMachine.isInteractingEvent.AddListener(EatObject);
    }

    private void OnDisable()
    {
        _alienStateMachine.isInteractingEvent.RemoveListener(EatObject);

    }
}