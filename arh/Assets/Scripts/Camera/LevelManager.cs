using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !(other.isTrigger))
        {
            virtualCam.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !(other.isTrigger))
        {
            virtualCam.SetActive(false);
        }
    }
    
}