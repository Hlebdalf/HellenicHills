using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreqButton : MonoBehaviour
{   
    private Text text;
    private void Start()
    {
        text = transform.GetChild(0).gameObject.GetComponent<Text>();
        if( PlayerPrefs.GetString("Freq") == name)
        {
            Green();
        }
        else
        {
            Red();
        }
    }
    public void Red()
    {
        text.color = new Color(1, 0, 0);
    }

    public void Green()
    {
        text.color = new Color(0, 1, 0);
        PlayerPrefs.SetString("Freq", name);
        PlayerPrefs.Save();
    }
}
