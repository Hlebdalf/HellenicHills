using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour
{
    public int m = 1;
    public float g = 10;
    public float angle = 45;
    void Start()
    {
        angle = 180 / Mathf.PI * angle;
        float forceY = -Mathf.Sin(angle) * m * g;
        float forceX = -Mathf.Cos(angle) * m * g;
        gameObject.GetComponent<Rigidbody>().AddForce(forceX,forceY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
