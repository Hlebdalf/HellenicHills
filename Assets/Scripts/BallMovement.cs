using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour
{
    public float g = 10;
    public float angle = 45;
    private float forceY;
    private float forceX;
    private float step = 0;
    void Start()
    {
        angle = 180 / Mathf.PI * angle;
        forceY = -Mathf.Sin(angle) * g;
        forceX = -Mathf.Cos(angle) * g;
        Physics.gravity = new Vector3(forceX, forceY, 0);
        //gameObject.GetComponent<Rigidbody>().Fo
        //gameObject.GetComponent<Rigidbody>().AddForce(forceX,forceY, 0, ForceMode.Impulse );
        //gameObject.GetComponent<Rigidbody>().AddForce(-forceX, -forceY, 0, ForceMode.Acceleration);
        //ForceMode myforce = new ForceMode.Ac
        //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, g, 0), ForceMode.Acceleration);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, new Vector3(forceX * 100, forceY * 100, 0), Color.red);
        //Physics.Simulate(step);
        step += 1f;
    }
}
