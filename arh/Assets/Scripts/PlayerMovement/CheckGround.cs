using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckGround : MonoBehaviour
{
    private float _checkRadius = 0.3f;
    [SerializeField] private Transform _groundPosition;
    [SerializeField] private LayerMask _groundLayer;
    public UnityEvent OnGroundedEvent;
    

    private void FixedUpdate()
    {
        var overlapCircle = Physics2D.OverlapCircle(_groundPosition.position, _checkRadius, _groundLayer);
        if (overlapCircle)
        {
            OnGroundedEvent.Invoke();
        }
    }
}
