using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitManager : MonoBehaviour
{
    
    [SerializeField] private UnityEvent noPlayerAtExit;
    [SerializeField] private UnityEvent onePlayerAtExit;
    [SerializeField] private UnityEvent bothPlayersAtExit;
    private readonly HashSet<GameObject> _playersAtExit = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !(other.isTrigger))
        {
            _playersAtExit.Add(other.gameObject);
            
            if (_playersAtExit.Count == 1)
            {
                onePlayerAtExit.Invoke();
            }
            
            if (_playersAtExit.Count == 2)
            {
                bothPlayersAtExit.Invoke();
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !(other.isTrigger))
        {
            _playersAtExit.Remove(other.gameObject);
        }

        if (_playersAtExit.Count == 0)
        {
            noPlayerAtExit.Invoke();
        }
    }
    
}
