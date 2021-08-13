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
            if (transform.position.y < 37)
            {
                volume.SetActive(true);
                panel.SetActive(true);
            }
            else
            {
                volume.SetActive(false);
                panel.SetActive(false);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            volume.SetActive(false); 
            panel.SetActive(false);
        }
    }
}
