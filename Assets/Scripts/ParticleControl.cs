using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParticleControl : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public AudioMixerSnapshot ground;
    public AudioMixerSnapshot air;
    public ParticleSystem particle;
    private Rigidbody rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        particle.startSpeed = rb.velocity.magnitude * 3;
        particle.emissionRate = rb.velocity.magnitude * 10;
        mixerGroup.audioMixer.SetFloat("PitchShift", Mathf.Lerp(0.5f, 1, rb.velocity.magnitude / 30));
        mixerGroup.audioMixer.SetFloat("LowPass", Mathf.Lerp(600, 8000, rb.velocity.magnitude / 30));

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            particle.startDelay = 0;
            ground.TransitionTo(0.1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Terrain")
        {
            air.TransitionTo(0.3f);
            particle.startDelay = 10000;
        }
    }
}
