using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GravityHandler : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float fallGravityMultiplier;

        private float _normalGravityScale;
        private float _isOnApex;

        [SerializeField] private ApexModifiers apexModifiers;

        public UnityEvent<float> GravityScaleChangedEvent;
        

        private void OnEnable()
        {
            _normalGravityScale = rb.gravityScale;
            
            apexModifiers.ApexChangedEvent.AddListener(OnApexChanged);
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        private void OnApexChanged(bool arg0)
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            if (rb.velocity.y < 0)
            {
                
            }
        }
    }
}