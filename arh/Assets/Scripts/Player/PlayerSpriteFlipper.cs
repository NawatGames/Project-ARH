using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlipper : MonoBehaviour
{
    private CoyoteScript coyoteScript;
    private bool facingRight;

    void Awake()
    {
        facingRight = true;
        coyoteScript = gameObject.GetComponent<CoyoteScript>();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void Update()
    {
        if(!facingRight)
        {
            if(coyoteScript.moveDirection > 0f)
            {
                Flip();
            }
        }

        else if(facingRight)
        {
            if(coyoteScript.moveDirection < 0f)
            {
                Flip();
            }
        }
    }

}
