using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PPButton : MonoBehaviour
{
    public Camera MainCamera;
    private bool isEnablePP = true;
    public void SetPPActivity()
    {
        isEnablePP = !isEnablePP;
        UniversalAdditionalCameraData uac = MainCamera.GetComponent<Camera>().GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        uac.renderPostProcessing = isEnablePP;
        if (isEnablePP) { gameObject.GetComponent<Image>().color = new Color(0, 1, 0); }
        else { gameObject.GetComponent<Image>().color = new Color(1, 0, 0); }     
    }
}
