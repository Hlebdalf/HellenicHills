using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FODamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Decorate"))
        {   
            Camera.main.GetComponent<FieldChecker>().FieldObjEvent("Decorates");
        }
    }
}
