using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControlSystem : MonoBehaviour
{
    private float speed =25;
    public void SetVelocityL()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
    }

    public void SetVelocityR()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
    }

    public void SetVelocityF()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
    }
    public void SetVelocityB()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
    }
    public void SetVelocity0()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
