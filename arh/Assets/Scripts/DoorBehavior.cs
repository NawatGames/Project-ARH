using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening.Core;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private float movementDuration;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private int buttonPressedNeeded;
    private int _buttonPressedCounter = 0;
    
    public void ButtomPressed()
    {
        if (isOpen == false)
        {
            _buttonPressedCounter++;
            
        }
        
    }
    public void DoorActivation()
    {
        if (isOpen == false)
        {
            if (_buttonPressedCounter == buttonPressedNeeded)
            {
                Debug.Log("Porta abriu!");
                isOpen = true;
            }
            
        }
        else
        {
            Debug.Log("Porta fechou!");
            isOpen = false;
        }
        
        
    }
}
