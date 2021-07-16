using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public GameObject ball;
    public GameObject AngleCube;
    public float speed;
    public int rate = 30;
    private int myRate;
    private Color backGround;
    private GameObject sun;
    private GameObject moon;
    private Light moonLight;
    private Light sunLight;
    private Light ballLight;
    private float angle;

    private void Start()
    {
        sun = transform.GetChild(0).gameObject;
        moon = transform.GetChild(1).gameObject;
        sunLight = sun.GetComponent<Light>();
        moonLight = moon.GetComponent<Light>();
        ballLight = ball.GetComponent<Light>();
        myRate = rate;
        StartCoroutine(RotatorCoroutine());
    }


    private IEnumerator RotatorCoroutine()
    {
        while (true)
        {
            angle = Quaternion.Angle(transform.rotation, AngleCube.transform.rotation) / 180;
            if (angle < 0.5f)
            {
                sun.SetActive(false);
                moon.SetActive(true);
                ball.SetActive(true);
                moonLight.intensity = (1 - angle)/4;
                ballLight.intensity = (1 - angle - 0.5f)*2;
                
            }
            else
            {
                ball.SetActive(false);
                sun.SetActive(true);
                moon.SetActive(false);
                sunLight.intensity = angle/1.2f;
            }
            backGround = new Color(angle,angle,angle);
            transform.Rotate(speed, 0, 0);
            Camera.main.backgroundColor = backGround;
            RenderSettings.fogColor = backGround;
            RenderSettings.ambientSkyColor = backGround;
            yield return new WaitForSeconds(1 / myRate);
        }
    }
}
