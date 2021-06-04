using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour
{   
    public float g = 10;
    public float angle = 45;
    public float TurnSpeed = 1;
    private float forceY;
    private float forceX;
    private Rigidbody rb;
    void Start()
    {
        angle = 180 / Mathf.PI * angle;
        forceY = -Mathf.Sin(angle) * g;
        forceX = -Mathf.Cos(angle) * g;
        rb = gameObject.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(forceX, forceY, 0);
    }

    public void StartMoveRight()
    {
        rb.AddForce(new Vector3(0, 0, -TurnSpeed), ForceMode.Acceleration);
    }
    public void EndMoveRight()
    {
        rb.velocity = Vector3.zero;
    }
    public void StartMoveLeft()
    {
        rb.AddForce(new Vector3(0, 0, TurnSpeed), ForceMode.Acceleration);
    }
    public void EndMoveLeft()
    {
        rb.velocity = Vector3.zero;
    }
}
