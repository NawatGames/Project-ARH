using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlienShrinkingScript : MonoBehaviour
{
    public bool alienShrunk;
    public Animator animator;

    void Awake()
    {
        alienShrunk = false;
    }

    public void Shrink(InputAction.CallbackContext context)
    {
        if(alienShrunk)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.32f, 0.32f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.0f);
            alienShrunk = false;
            animator.SetBool("IsShrinking", false);
            animator.SetBool("ShrinkingEnded", false);
        }

        else
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.32f, 0.16f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, -0.08f);
            alienShrunk = true;
            animator.SetBool("IsShrinking", true);
            StartCoroutine(ShrinkCountdown());
        }
    }

    IEnumerator ShrinkCountdown()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("ShrinkingEnded", true);
    }
}
