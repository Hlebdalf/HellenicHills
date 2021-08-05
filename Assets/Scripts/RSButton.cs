using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using UnityEngine.Rendering.Universal;

public class RSButton : MonoBehaviour
{
    public Light sunLight;
    public Light moonLight;
    private bool isEnableRS = true;
    public void SetRSActivity()
    {
        
        isEnableRS = !isEnableRS;
        if (isEnableRS) { 
            gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
            sunLight.shadows = LightShadows.Hard;
            moonLight.shadows = LightShadows.Hard;
        }
        else { 
            gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
            sunLight.shadows = LightShadows.None;
            moonLight.shadows = LightShadows.None;
        }
    }
}
