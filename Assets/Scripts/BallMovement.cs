using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class BallMovement : MonoBehaviour
{   
    public float turnForce = 1;
    public float forwardForce = 10;
    [FormerlySerializedAs("InertiaDivider")] public float inertiaDivider = 2;
    [FormerlySerializedAs("TurnSpeedRoof")] public float turnSpeedRoof = 50;
    [FormerlySerializedAs("ForwardSpeedRoof")] public float forwardSpeedRoof = 100;
    private Rigidbody _rb;
    private bool _moovingRight = false;
    private bool _moovingLeft = false;
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
}
