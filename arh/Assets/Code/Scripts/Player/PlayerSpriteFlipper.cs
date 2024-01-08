using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlipper : MonoBehaviour
{
    private PlayerMovementTest1 moveScript;
    private bool facingRight;

    void Awake()
    {
        facingRight = true;
        moveScript = gameObject.GetComponent<PlayerMovementTest1>();
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
            if(moveScript.moveDirection > 0f)
            {
                Flip();
            }
        }

        else if(facingRight)
        {
            if(moveScript.moveDirection < 0f)
            {
                Flip();
            }
        }
    }

}
