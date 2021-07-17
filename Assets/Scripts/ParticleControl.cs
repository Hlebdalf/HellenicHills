using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            particle.startDelay = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Terrain")
        {
            particle.startDelay = 10000;
        }
    }
}
