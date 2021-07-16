using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public Gradient SunColor;
    public Gradient FogColorUp;
    public Gradient FogColorDown;
    public Gradient SunStrenght;
    public Gradient GorizontColorUp;
    public Gradient SkyColorUp;
    public Gradient GorizontColorDown;
    public Gradient SkyColorDown;
    public GameObject ball;
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
    private float angle1;
    [SerializeField]
    private bool upOrDown = true;
    private Vector3 up;
    private void Start()
    {
        up = transform.forward;
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
            angle = Quaternion.Angle(transform.rotation, new Quaternion(0, 0, 0, 1)) / 180;
            angle1 = -Vector3.SignedAngle(up, transform.up, transform.right) / 180;
            if (angle < 0.5f && upOrDown)
            {
                upOrDown = false;
                sun.SetActive(false);
                moon.SetActive(true);
                ball.SetActive(true);
            }

            else if (angle > 0.5f && !upOrDown)
            {
                ball.SetActive(false);
                sun.SetActive(true);
                moon.SetActive(false);
                upOrDown = true;
            }
            if (upOrDown)
            {
                sunLight.intensity = SunStrenght.Evaluate(angle1).g;
                sunLight.color = SunColor.Evaluate(angle1);
                RenderSettings.ambientSkyColor = SkyColorUp.Evaluate(angle1);
                RenderSettings.ambientEquatorColor = GorizontColorUp.Evaluate(angle1);
                Camera.main.backgroundColor = FogColorUp.Evaluate(angle1);
                RenderSettings.fogColor = FogColorUp.Evaluate(angle1);
            }

            else
            {
                moonLight.intensity = (1 - angle) / 4;
                ballLight.intensity = (1 - angle - 0.5f) * 2;
                RenderSettings.ambientSkyColor = SkyColorDown.Evaluate(angle1 + 1);
                RenderSettings.ambientEquatorColor = GorizontColorDown.Evaluate(angle1 + 1);
                Camera.main.backgroundColor = FogColorDown.Evaluate(angle1 + 1);
                RenderSettings.fogColor = FogColorDown.Evaluate(angle1 + 1);
            }
            backGround = new Color(angle, angle, angle);
            transform.Rotate(speed, 0, 0);
            yield return new WaitForSeconds(1 / myRate);
        }
    }
}
