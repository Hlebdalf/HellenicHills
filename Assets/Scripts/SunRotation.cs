using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float speed;
    public int rate = 30;
    private int myRate;
    private Color backGround;
    private GameObject light;
    private void Start()
    {
        light = transform.GetChild(0).gameObject;
        myRate = rate;
        StartCoroutine(RotatorCoroutine());
    }
    private IEnumerator RotatorCoroutine()
    {
        while (true)
        {
            backGround = new Color((transform.rotation.x + 1) / 2, (transform.rotation.x + 1) / 2, (transform.rotation.x + 1) / 2);
            if ((transform.rotation.x + 1) / 2 < 0.5f)
            {
                light.GetComponent<Light>().intensity = 0;
                myRate = 2 * rate;
            }
            else
            {
                light.GetComponent<Light>().intensity = (transform.rotation.x + 1) / 2;
                myRate = rate;
            }
            transform.Rotate(speed, 0, 0);
            Camera.main.backgroundColor = backGround;
            RenderSettings.fogColor = backGround;
            RenderSettings.ambientSkyColor = (new Color(0.3f, 0.3f, 0.3f) +backGround)/2;
            yield return new WaitForSeconds(1 / myRate);
        }
    }
}
