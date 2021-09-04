using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PPButton : MonoBehaviour
{
    public Camera MainCamera;
    private bool _isEnablePP;
    private void Start(){
        if(PlayerPrefs.GetInt("isEnablePP") == 1)
        {
            _isEnablePP = true;
        } 
        else 
        {
            _isEnablePP = false;
        }
        Refresh();
    }
    public void SetPPActivity()
    {
        _isEnablePP = !_isEnablePP;
        Refresh();
    }

    private void Refresh()
    {
        UniversalAdditionalCameraData uac = MainCamera.GetComponent<Camera>().GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        uac.renderPostProcessing = _isEnablePP;
        if (_isEnablePP) 
        { 
            gameObject.GetComponent<Image>().color = new Color(0, 1, 0); 
            PlayerPrefs.SetInt("isEnablePP", 1);
        }
        else 
        { 
            gameObject.GetComponent<Image>().color = new Color(1, 0, 0); 
            PlayerPrefs.SetInt("isEnablePP", 0);
        } 
        PlayerPrefs.Save();   
    }
}
