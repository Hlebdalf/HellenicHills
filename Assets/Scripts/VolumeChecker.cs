using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChecker : MonoBehaviour
{
    public GameObject volume;
    public GameObject panel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {   
            Camera.main.GetComponent<FieldChecker>().FieldObjEvent("EnterWater");
            volume.SetActive(true);
            panel.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {   
            Camera.main.GetComponent<FieldChecker>().FieldObjEvent("ExitWater");
            volume.SetActive(false); 
            panel.SetActive(false);
        }
    }
}
