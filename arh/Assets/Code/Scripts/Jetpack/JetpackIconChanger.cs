using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackIconChanger : MonoBehaviour
{
    private JetpackPlayerMovement jetpackMovement;
    public GameObject player;
    public GameObject icon;

    private void Start()
    {
        jetpackMovement = player.GetComponent<JetpackPlayerMovement>();
    }

    private void Update()
    {
        if(jetpackMovement.jetpackEnabled)
        {
            icon.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }
}
