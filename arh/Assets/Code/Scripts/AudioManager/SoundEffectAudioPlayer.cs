using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectAudioPlayer : MonoBehaviour
{
    
    [SerializeField] AudioSource jump;
    [SerializeField] AudioClip jumpSound;


    public void PlayJump()
    {
        jump.PlayOneShot(jumpSound);
    }

}
