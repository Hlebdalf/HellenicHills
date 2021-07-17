using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldObjScript : MonoBehaviour
{
    public Death death;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Machine_ball" && !other.isTrigger)
        {
            death.FieldObjEvent(name);
            if (gameObject.GetComponent<CapsuleCollider>() != null)
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

}