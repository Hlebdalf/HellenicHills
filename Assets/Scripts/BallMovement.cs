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
        rb.AddForce(new Vector3(0, 0, -TurnSpeed), ForceMode.Force);
    }
    public void EndMoveRight()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }
    public void StartMoveLeft()
    {
        rb.AddForce(new Vector3(0, 0, TurnSpeed), ForceMode.Force);
    }
    public void EndMoveLeft()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }
}
