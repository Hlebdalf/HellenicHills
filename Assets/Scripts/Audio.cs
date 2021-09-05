using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioSource hit1;
    public AudioSource hit2;
    public void HitSound(float val)
    {
        hit1.volume = val;
        hit1.pitch = val;
        hit1.Play();

        hit2.volume = val;
        hit2.pitch = val;
        hit2.Play();
    }
}
