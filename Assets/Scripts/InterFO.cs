using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterFO : MonoBehaviour
{
    private Vector3 up;
    void Start()
    {
        up = transform.up;
        float coin = Random.value;
        transform.LookAt(transform.parent.GetComponent<FieldObject>().normal + transform.position - up);
        transform.Rotate(0, coin * 360, 0);
    }
}
