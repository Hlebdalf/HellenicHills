using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource music;
    public AudioSource button;
    public AudioSource motor;
    public AudioSource water;
    public AudioSource end;
    public AudioSource coin;
    public AudioSource typing;
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
        how = !how;
        music.mute = how;
    }

    public void PlaySound(bool how)
    {
        how = !how;
        typing.mute = how;
        hit1.mute = how;
        hit2.mute = how;
        motor.mute = how;
        button.mute = how;
        water.mute = how;
        end.mute = how;
        coin.mute = how;
    }

    public void CoinSound()
    {
        coin.Play();
    }
}
