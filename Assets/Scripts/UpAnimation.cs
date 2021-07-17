using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAnimation : MonoBehaviour
{
    public float speed = 2;
    void FixedUpdate()
    {
        transform.Rotate(0, speed, 0);
    }
    
}
