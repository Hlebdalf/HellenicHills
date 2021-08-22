using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChecker : MonoBehaviour
{
    public GameObject volume;
    public GameObject panel;
    private bool _upOrDown = true;

    private void Start()
    {
        StartCoroutine(CheckVolume());
    }

    private IEnumerator CheckVolume()
    {   
        yield return new WaitForSeconds(2);
        while (true)
        {
            if (transform.position.y < 32)
            {
                if (_upOrDown)
                {   
                    _upOrDown = false;
                    VolumeSetActive();
                }
                _upOrDown = false;
            }
            else
            {
                if (!_upOrDown)
                {
                    _upOrDown = true;
                    VolumeSetActive();
                }
                _upOrDown = true;
            }
            yield return new WaitForSeconds(0.066f);
        }
    }

    private void VolumeSetActive()
    {
        if (_upOrDown)
        {
            GetComponent<FieldChecker>().FieldObjEvent("ExitWater");
            volume.SetActive(false); 
            panel.SetActive(false);
        }
        else
        {
            GetComponent<FieldChecker>().FieldObjEvent("EnterWater");
            volume.SetActive(true);
            panel.SetActive(true);
        }
    }
}
