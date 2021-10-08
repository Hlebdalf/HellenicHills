using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RSButton : MonoBehaviour
{
    public Light sunLight;
    public Light moonLight;
    private bool _isEnableRS;
    private void Start()
    {
        if (PlayerPrefs.GetInt("isEnableRS") == 1) _isEnableRS = true;
        else _isEnableRS = false;
        Refresh();
    }
    public void SetRSActivity()
    {
        _isEnableRS = !_isEnableRS;
        Refresh();
    }
    public void Refresh()
    {
        if (_isEnableRS)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
            sunLight.shadows = LightShadows.Hard;
            moonLight.shadows = LightShadows.Hard;
            PlayerPrefs.SetInt("isEnableRS", 1);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
            sunLight.shadows = LightShadows.None;
            moonLight.shadows = LightShadows.None;
            PlayerPrefs.SetInt("isEnableRS", 0);

            PlayerPrefs.Save();
        }
    }
}
