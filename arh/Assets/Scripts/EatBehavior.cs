using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EatBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _edibleObject;

    [SerializeField] private bool _objectIsInRange = false;
    [SerializeField] private bool _eatButtonWasPressed = false;
    [SerializeField] private bool _objectWasAte;

    [SerializeField] private float _throwForce;
    private Color _edibleObjectBaseColor;
    private Rigidbody2D _edibleObjectRigidBody;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_objectWasAte == false)
        {
            if (_edibleObject != null)
            {
                if (_edibleObject.GetComponent<Rigidbody2D>() != null)
                {
                    _edibleObjectRigidBody = _edibleObject.GetComponent<Rigidbody2D>();
                    
                    if (_eatButtonWasPressed == true)
                    {
                        if (_objectIsInRange == true)
                        {
                            // Consome o Objeto
                            _objectWasAte = true;
                            _eatButtonWasPressed = false;
                            _edibleObject.SetActive(false);
                            Debug.Log("Comi o Objeto");
                }

                    }
                }
            }
        }

        if (_objectWasAte == true)
        {
            _edibleObject.transform.position = gameObject.transform.position;
            if (_eatButtonWasPressed == true)
            {
                // Cospe o Objeto com uma certa força
                _objectWasAte = false;
                _eatButtonWasPressed = false;
                _edibleObject.SetActive(true);

                _edibleObject.transform.position = _edibleObject.transform.position;
                _edibleObjectRigidBody.velocity = new Vector2(_throwForce, _edibleObjectRigidBody.velocity.y);

                Debug.Log("Cuspi o Objeto");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_objectWasAte)
        {
            if (_objectIsInRange == false)
            {
                if (other.CompareTag("Edible Object"))
                {
                    _edibleObject = other.gameObject;

                    _objectIsInRange = true;
                    Debug.Log("encostei em algo!");
                }
                    
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_objectWasAte)
        {
            _objectIsInRange = false;
            _edibleObject = null;
        }
    }
}