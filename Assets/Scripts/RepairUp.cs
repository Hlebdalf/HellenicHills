using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUp : MonoBehaviour
{   
    private Transform _ball;
    private Vector3 _forward;
    private void Start() 
    {   
        _forward = transform.forward;
        _ball = Camera.main.GetComponent<ModelTypeLoad>().ballTransform;
        RotateUp();
    }
    public void RotateUp()
    {
        StartCoroutine(Rotator());
    }

    IEnumerator Rotator()
    {   
        while(true)
        {   
            Vector3 target = new Vector3(_ball.position.x + transform.right.x, transform.position.y, _ball.position.z + transform.right.z);
            //Vector3 target = _ball.position - transform.right;
            transform.LookAt(target);
            yield return new WaitForFixedUpdate();
        }
    }
}
