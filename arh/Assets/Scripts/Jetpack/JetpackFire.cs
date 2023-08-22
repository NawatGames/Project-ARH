using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackFire : MonoBehaviour
{
    private JetpackPlayerMovement jetpackScript;
    public float fireTimer;
    public ParticleSystem fireParticles;

    void Start()
    {
        jetpackScript = gameObject.GetComponent<JetpackPlayerMovement>();
        fireParticles.Stop();
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(jetpackScript.doublejumpEnabled)
            {
                if(jetpackScript.inAir)
                {
                    fireParticles.Play();
                    StartCoroutine(FireCountdown());
                }
            }
        }
    }

    IEnumerator FireCountdown()
    {
        yield return new WaitForSeconds(fireTimer);
        fireParticles.Stop();
    }
}
