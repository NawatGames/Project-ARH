using UnityEngine;
using UnityEngine.Events;

public class Apogee : MonoBehaviour
{
    public UnityEvent ReachedApogeeEvent;

    [SerializeField] private Rigidbody2D _rigidbody;

    private float _previousVelocity;
    private float _currentVelocity;

    private void FixedUpdate()
    {
        _currentVelocity = _rigidbody.velocity.y;
        if (_previousVelocity > 0 && _currentVelocity <= 0)
        {
            ReachedApogeeEvent.Invoke();
        }
        _previousVelocity = _currentVelocity;
    }
}
