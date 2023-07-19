using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckIfPlayerOn : MonoBehaviour
{
    [SerializeField] private UnityEvent playerOnPressurePlate;
    [SerializeField] private UnityEvent playerOffPressurePlate;
    private HashSet<GameObject> _playersOnPressurePlate;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (_playersOnPressurePlate.Count == 0)
            {
                playerOnPressurePlate.Invoke();
            }

            _playersOnPressurePlate.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _playersOnPressurePlate.Remove(other.gameObject);

            if (_playersOnPressurePlate.Count == 0)
            {
                playerOffPressurePlate.Invoke();
            }
        }
    }
    
    public void PlayerOn()
    {
        Debug.Log("Player em cima da placa de pressão");
    }
    
    public void PlayerOff()
    {
        Debug.Log("Player saiu da placa de pressão");
    }
}
