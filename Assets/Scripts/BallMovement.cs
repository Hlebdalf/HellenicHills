using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;


public class BallMovement : MonoBehaviour
{   
    public Slider slider;
    public Image button;
    public float turnForce = 1;
    public float forwardForce = 10;
    [FormerlySerializedAs("InertiaDivider")] public float inertiaDivider = 2;
    [FormerlySerializedAs("TurnSpeedRoof")] public float turnSpeedRoof = 50;
    [FormerlySerializedAs("ForwardSpeedRoof")] public float forwardSpeedRoof = 100;
    private Rigidbody _rb;
    private bool _moovingRight = false;
    private bool _moovingLeft = false;
    private bool _isBoost = false;
    public float boost = 1000;
    public float boostCons = 10;
    public float boostGrow = 1;
    public float boostMP = 1;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(ForwardCoroutine());
    }

    public void StartMoveRight()
    {
        _moovingRight = true;
        StartCoroutine(RightGrowCoroutine());
    }
    public void EndMoveRight()
    {
        _moovingRight = false;
    }
    public void StartMoveLeft()
    {
        _moovingLeft = true;
        StartCoroutine(LeftGrowCoroutine());
    }
    public void EndMoveLeft()
    {
        _moovingLeft = false;
    }

    IEnumerator LeftGrowCoroutine()
    {
        while (_moovingLeft)
        {
            if (_rb.velocity.z > turnSpeedRoof)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, turnSpeedRoof);
            }
            else
            {
                if (_rb.velocity.z < 0)
                {
                    Vector3 velocityDevider = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z / inertiaDivider);
                    _rb.velocity = velocityDevider;
                }
                _rb.AddForce(new Vector3(0, 0, turnForce));
            }
            // yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.0083f);
        }
    }

    IEnumerator RightGrowCoroutine()
    {
        while (_moovingRight)
        {
            if (_rb.velocity.z < -turnSpeedRoof)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, -turnSpeedRoof);
            }
            else
            {
                if (_rb.velocity.z > 0)
                {
                    Vector3 velocityDevider = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z / inertiaDivider);
                    _rb.velocity = velocityDevider;
                }
                _rb.AddForce(new Vector3(0, 0, -turnForce));
            }
            //yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.0083f);
        }
    }

    IEnumerator ForwardCoroutine()
    {
        while (true)
        //Debug.Log(_rb.velocity.magnitude);
        {
            if (_rb.velocity.x > forwardSpeedRoof)
            {
                _rb.velocity = new Vector3(forwardSpeedRoof, _rb.velocity.y, _rb.velocity.z);
            }
            else
            {
                _rb.AddForce(new Vector3(forwardForce, 0, 0));
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void OnBoost(){
        if (!_isBoost)
        {
        button.color = new Color(0.3f, 0.3f, 0.3f);
        _isBoost = true;
        forwardForce *= boostMP;
        turnForce *= boostMP;
        StartCoroutine(BoostConsumption());
        }
    }

    IEnumerator BoostConsumption()
    {
        while(boost > 0){
            slider.value = boost;
            boost -= boostCons;
            yield return new WaitForFixedUpdate();
        }
        forwardForce /= boostMP;
        turnForce /= boostMP;
        StartCoroutine(BoostGrow());
    }

    IEnumerator BoostGrow()
    {
        while(boost < 1000){
            boost += boostGrow;
            slider.value = boost;
            yield return new WaitForFixedUpdate();
        }
        _isBoost = false;
        button.color = new Color(0.85f, 1, 0);
    }
}
