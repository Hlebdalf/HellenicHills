using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSlant : MonoBehaviour
{
    public GameObject ball;
    public GameObject machineUp;
    private Rigidbody rb;
    private int mp = 1;
    private float angle = 0;
    private float preangle = 0;
    private float delta = 0;
    Vector3 right;
    void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
        right = transform.right;

    }
    void Update()
    {
        if (right.z > rb.velocity.z) mp = 1;
        else mp = -1;
        angle = Vector3.Angle(right, new Vector3(rb.velocity.x, 0, rb.velocity.z))*mp;
        delta = angle - preangle;
        transform.rotation = Quaternion.Euler(0, angle, rb.velocity.magnitude/3);
        machineUp.transform.Rotate(0, -delta, 0);
        preangle = angle;
    }
}
