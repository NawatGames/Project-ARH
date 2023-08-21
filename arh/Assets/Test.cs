using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private new Collider2D collider2D;
    
    public void TellPlayerShouldGoToExit()
    {
        print("Segundo player deve ir até o fim da fase");
    }

    public void DisableExitCollider()
    {
        collider2D.enabled = false;
    }
    
    public void EnableExitCollider()
    {
        collider2D.enabled = true;
    }
}
