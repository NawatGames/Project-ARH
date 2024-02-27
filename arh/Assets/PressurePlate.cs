using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Placa ativada");
        gameObject.transform.localScale -= new Vector3(0f,0.25f,0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameObject.transform.localScale += new Vector3(0f,0.25f,0f);
    }
}