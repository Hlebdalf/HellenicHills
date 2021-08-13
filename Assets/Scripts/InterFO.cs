using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterFO : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Machine_ball")
        {
           Camera.main.GetComponent<FieldChecker>().FieldObjEvent(name);
           GetComponent<BoxCollider>().enabled = false;
        }
    }
}
