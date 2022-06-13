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
        Application.targetFrameRate = 120;
    }
    void Update()
    {
        float fps = (int)(1.0f / Time.deltaTime);
        gameObject.GetComponent<Text>().text = fps.ToString();
    }

    public void SetFreq(int freq)
    {
        Application.targetFrameRate = freq;
    }
}