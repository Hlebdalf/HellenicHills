using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FODamage : MonoBehaviour
{
    public float health = 1000;
    public Slider slider;
    public float mp = 1;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Decorate"))
        {
            health -= _rb.velocity.magnitude * mp;
            _rb.velocity = _rb.velocity / 2;
            slider.value = health;
            if (health <= 0)
            {
                Camera.main.GetComponent<FieldChecker>().GameOver();
            }
        }
    }

    public void RepairMachine()
    {
        health = 1000;
        slider.value = health;
    }
}
