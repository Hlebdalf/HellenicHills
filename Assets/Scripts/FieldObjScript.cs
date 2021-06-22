using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldObjScript : MonoBehaviour
{
    public Death death;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Machine_ball")
        {
            death.FieldObjEvent(name);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

}