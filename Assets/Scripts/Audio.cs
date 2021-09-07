using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource music;
    private bool _isMusic = true;
    public void HitSound(float val)
    {
        hit1.volume = val;
        hit1.pitch = val;
        hit1.Play();
        Handheld.Vibrate();
        hit2.volume = val;
        hit2.pitch = val;
        hit2.Play();
    }

    public void PlayMusic(bool how)
    {
        if (how) 
        {
            music.mute = false;
        }
        else 
        {
            music.mute = true;
        }
    }
}
