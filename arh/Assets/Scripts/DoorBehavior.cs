using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using DG.Tweening.Core;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private float movementDuration;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private int buttonPressedNeeded;
    public int _buttonPressedCounter = 0;


    private void Awake()
    {
        //_movementDirection = transform.localPosition;
    }

    public void ButtomPressed()
    { 
        _buttonPressedCounter++;
        DoorToogle();
    }

    public void ButtonUnpressed()
    {
        _buttonPressedCounter--;
        DoorToogle();

    }
    public void DoorToogle()
    {
        if (isOpen == false)
        {
            if (_buttonPressedCounter == buttonPressedNeeded)
            {
                transform.DOMoveY(4f, movementDuration, false);
               //Debug.Log("Porta abriu!");
                isOpen = true;
            }
            
        }
        else
        {
            transform.DOMoveY(0f, movementDuration, false);
            //Debug.Log("Porta fechou!");
            isOpen = false;
        }
        
        
    }
}
