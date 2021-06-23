using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityUI : MonoBehaviour
{
    public Image R;
    public Image L;
    public GameObject Ball;
    private Rigidbody rb;
    void Start()
    {
        rb = Ball.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float zVel = rb.velocity.z;
        if (zVel > 0)
        {
            L.rectTransform.sizeDelta = new Vector2(rb.velocity.z*15, 15);
            R.rectTransform.sizeDelta = new Vector2(0, 15);
        }
        else
        {
            R.rectTransform.sizeDelta = new Vector2(-rb.velocity.z*15, 15);
            L.rectTransform.sizeDelta = new Vector2(0, 15);
        }
        
        
    }
}
