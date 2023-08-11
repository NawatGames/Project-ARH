using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ApexModifiers : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float threshold;
    private bool _isOnApex;
    private bool _isGrounded;

    [SerializeField] private GroundedHandler groundedHandler;
    
    public UnityEvent<bool> ApexChangedEvent;

    private void OnEnable()
    {
        groundedHandler.IsGroundedChangedEvent.AddListener(OnIsGroundedChanged);
    }
    
    private void OnDisable()
    {
        groundedHandler.IsGroundedChangedEvent.RemoveListener(OnIsGroundedChanged);
    }

    private void OnIsGroundedChanged(bool arg0)
    {
        _isGrounded = arg0;
    }


    void Update()
    {
        if (Mathf.Abs(rb.velocity.y) < threshold && !_isGrounded)
        {
            _isOnApex = true;
            ApexChangedEvent.Invoke(_isOnApex);
        }
        else
        {
            _isOnApex = false;
            ApexChangedEvent.Invoke(_isOnApex);
        }
    }
}
