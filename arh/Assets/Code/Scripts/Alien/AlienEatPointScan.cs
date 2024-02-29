using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlienEatPointScan : MonoBehaviour
{
    [SerializeField] private AlienStateMachine _alienStateMachine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_alienStateMachine.hasStoredObject && !_alienStateMachine.isEdibleInRange && other.CompareTag("Food"))
        {
            _alienStateMachine.currentEdibleObject = other.gameObject;

            _alienStateMachine.isEdibleInRange = true;
            //Debug.Log("Food Is In Range!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_alienStateMachine.hasStoredObject)
        {
            _alienStateMachine.isEdibleInRange = false;
            _alienStateMachine.currentEdibleObject = other.gameObject;
        }
    }
}