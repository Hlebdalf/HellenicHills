using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargerScript : MonoBehaviour
{
    public Death death;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Machine_ball")
        {
            death.StartCharge();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
