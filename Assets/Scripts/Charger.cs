using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Machine_ball")
        {
           Camera.main.GetComponent<FieldChecker>().FieldObjEvent("Charge");
           GetComponent<BoxCollider>().enabled = false;
        }
    }
}
