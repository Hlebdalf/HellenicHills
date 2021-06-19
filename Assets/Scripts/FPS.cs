using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 90;
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        gameObject.GetComponent<Text>().text = fps.ToString();
    }
}
