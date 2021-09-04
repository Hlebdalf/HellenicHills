using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParticleControl : MonoBehaviour
{   
    public AudioMixerGroup mixerGroup;
    public AudioMixerSnapshot airSnap;
    public AudioMixerSnapshot groundSnap;
    public ParticleSystem particle;
    private Rigidbody rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        airSnap.TransitionTo(0.3f);  
    }
    private void FixedUpdate()
    {
        particle.startSpeed = rb.velocity.magnitude * 3;
        particle.emissionRate = rb.velocity.magnitude * 10;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            particle.startDelay = 0;
            groundSnap.TransitionTo(0.1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Terrain")
        {
            particle.startDelay = 10000;
            airSnap.TransitionTo(0.3f);          
        }
    }
}
