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
    private float _height;
    void Start()
    {
        rb = Ball.GetComponent<Rigidbody>();
        _height = 26.7f;
    }

    void FixedUpdate()
    {
        float zVel = rb.velocity.z;
        if (zVel > 0)
        {
            L.rectTransform.sizeDelta = new Vector2(rb.velocity.z * 10.3f, _height);
            R.rectTransform.sizeDelta = new Vector2(0, _height);
        }
        else
        {
            R.rectTransform.sizeDelta = new Vector2(-rb.velocity.z * 10.3f, _height);
            L.rectTransform.sizeDelta = new Vector2(0, _height);
        }
        
        
    }
}
