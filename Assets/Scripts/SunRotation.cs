using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SunRotation : MonoBehaviour
{
    [FormerlySerializedAs("SunColor")] public Gradient sunColor;
    [FormerlySerializedAs("FogColorUp")] public Gradient fogColorUp;
    [FormerlySerializedAs("FogColorDown")] public Gradient fogColorDown;
    [FormerlySerializedAs("SunStrenght")] public Gradient sunStrenght;
    [FormerlySerializedAs("MoonStrenght")] public Gradient moonStrenght;
    [FormerlySerializedAs("GorizontColorUp")] public Gradient gorizontColorUp;
    [FormerlySerializedAs("SkyColorUp")] public Gradient skyColorUp;
    [FormerlySerializedAs("GorizontColorDown")] public Gradient gorizontColorDown;
    [FormerlySerializedAs("SkyColorDown")] public Gradient skyColorDown;
    public GameObject ball;
    public float speed;
    private float _mySpeed;
    public int rate = 30;
    private int _myRate;
    private Color _backGround;
    private GameObject _sun;
    private GameObject _moon;
    private Light _moonLight;
    private Light _sunLight;
    private Light _ballLight;
    private float _angle;
    private float _angle1;
    [SerializeField]
    private bool upOrDown = true;
    private Vector3 _up;
    private void Start()
    {   
        float coin = Random.value;
        _up = transform.forward;
        _sun = transform.GetChild(0).gameObject;
        _moon = transform.GetChild(1).gameObject;
        _sunLight = _sun.GetComponent<Light>();
        _moonLight = _moon.GetComponent<Light>();
        _ballLight = ball.GetComponent<Light>();
        _myRate = rate;
        _mySpeed = speed;
        transform.Rotate(coin * 360, 0, 0);
        StartCoroutine(RotatorCoroutine());

    }

    private IEnumerator RotatorCoroutine()
    {
        while (true)
        {   
            _angle = -Mathf.Acos(transform.up.z) * Mathf.Abs(Mathf.Asin(transform.up.y)) / Mathf.Asin(transform.up.y) / Mathf.PI;
            _angle1 = -Vector3.SignedAngle(_up, transform.up, transform.right) / 180;
            //Debug.Log(_angle);
            if (_angle < 0 && upOrDown)
            {
                upOrDown = false;
                _sun.SetActive(false);
                _moon.SetActive(true);
                ball.SetActive(true);
            }

            else if (_angle > 0 && !upOrDown)
            {
                ball.SetActive(false);
                _sun.SetActive(true);
                _moon.SetActive(false);
                upOrDown = true;
            }
            if (upOrDown)
            {
                _sunLight.intensity = sunStrenght.Evaluate(_angle).g;
                _sunLight.color = sunColor.Evaluate(_angle);
                RenderSettings.ambientSkyColor = skyColorUp.Evaluate(_angle);
                RenderSettings.ambientEquatorColor = gorizontColorUp.Evaluate(_angle);
                Camera.main.backgroundColor = fogColorUp.Evaluate(_angle);
                RenderSettings.fogColor = fogColorUp.Evaluate(_angle);
                _mySpeed = speed;
            }
            else
            {   
                _angle = 1-Mathf.Abs(_angle);
                _moonLight.intensity = moonStrenght.Evaluate(_angle).g;
                //_ballLight.intensity = (_angle + 1) * 2;
                RenderSettings.ambientSkyColor = skyColorDown.Evaluate(_angle);
                RenderSettings.ambientEquatorColor = gorizontColorDown.Evaluate(_angle);
                Camera.main.backgroundColor = fogColorDown.Evaluate(_angle);
                RenderSettings.fogColor = fogColorDown.Evaluate(_angle);
                _mySpeed = speed * 2;
            }
            _backGround = new Color(_angle, _angle, _angle);
            transform.Rotate(_mySpeed, 0, 0);
            yield return new WaitForSeconds(1 / _myRate);
        }
    }
}
