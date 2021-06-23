using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour
{
    public float TurnForce = 1;
    public float ForwardForce = 10;
    public float InertiaDivider = 2;
    public float TurnForceDevider = 4;
    public float TurnSpeedRoof = 50;
    public float ForwardSpeedRoof = 100;
    private float turnForce;
    private float forwardForce;
    private Rigidbody rb;
    private bool moovingRight = false;
    private bool moovingLeft = false;
    void Start()
    {
        forwardForce = 0;
        turnForce = 0;
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (rb.velocity.x > ForwardSpeedRoof)
        {
            rb.velocity = new Vector3(ForwardSpeedRoof, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.AddForce(new Vector3(forwardForce, 0, 0));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            forwardForce = ForwardForce;
            turnForce = TurnForce;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Terrain")
        {
            forwardForce = 0;
            turnForce = TurnForce / TurnForceDevider;
        }
    }
    public void StartMoveRight()
    {
        moovingRight = true;
        StartCoroutine(RightGrowCoroutine());
    }
    public void EndMoveRight()
    {
        moovingRight = false;
    }
    public void StartMoveLeft()
    {
        moovingLeft = true;
        StartCoroutine(LeftGrowCoroutine());
    }
    public void EndMoveLeft()
    {
        moovingLeft = false;
    }

    IEnumerator LeftGrowCoroutine()
    {
        while (moovingLeft)
        {
            if (rb.velocity.z > TurnSpeedRoof)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, TurnSpeedRoof);
            }
            else
            {
                if (rb.velocity.z < 0)
                {
                    Vector3 velocityDevider = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z / InertiaDivider);
                    rb.velocity = velocityDevider;
                }
                rb.AddForce(new Vector3(0, 0, turnForce));
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator RightGrowCoroutine()
    {
        while (moovingRight)
        {
            if (rb.velocity.z < -TurnSpeedRoof)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -TurnSpeedRoof);
            }
            else
            {
                if (rb.velocity.z > 0)
                {
                    Vector3 velocityDevider = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z / InertiaDivider);
                    rb.velocity = velocityDevider;
                }
                rb.AddForce(new Vector3(0, 0, -turnForce));
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
