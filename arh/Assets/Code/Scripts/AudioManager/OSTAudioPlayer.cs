using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource ost1;
    [SerializeField] AudioSource ost2;
    [SerializeField] AudioSource ost3;
    [SerializeField] AudioSource ost4;
    [SerializeField] AudioSource ost5;

    public void PlayOst1()
    {
        ost1.Play();
    }
    public void PlayOst2()
    {
        ost2.Play();
    }
    public void PlayOst3()
    {
        ost3.Play();
    }
    public void PlayOst4()
    {
        ost4.Play();
    }
    public void PlayOst5()
    {
        ost5.Play();
    }

    private void ChangeOST(AudioSource playingOst, AudioSource newOst)
    {
        playingOst.Stop();
        playingOst.clip = newOst.clip;
        playingOst.Play();
    }
}
