using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    private void Start()
    {
        int screenHeight = (Screen.height * 1080) / Screen.width;
        Screen.SetResolution(1080, screenHeight, true);
        Application.targetFrameRate = int.Parse(PlayerPrefs.GetString("Freq", "60"));
    }
    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        gameObject.GetComponent<Text>().text = fps.ToString();
    }

    public void SetFreq(int freq)
    {
        Application.targetFrameRate = freq;
    }
}
