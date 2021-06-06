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
    private bool moovingRight = false;
    private bool moovingLeft = false;
    void Start()
    {
        angle = 180 / Mathf.PI * angle;
        forceY = -Mathf.Sin(angle) * g;
        forceX = -Mathf.Cos(angle) * g;
        rb = gameObject.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(forceX, forceY, 0);    }

    public void StartMoveRight()
    {
        moovingRight = true;
        StartCoroutine(RightGrowCoroutine());  
    }
    public void EndMoveRight()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
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
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }

    IEnumerator LeftGrowCoroutine()
    {
        while (moovingLeft)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, TurnSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator RightGrowCoroutine()
    {
        while (moovingRight)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -TurnSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
}
