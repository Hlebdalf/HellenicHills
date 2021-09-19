using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUp : MonoBehaviour
{   
    private Transform _ball;    private Vector3 _forward;
    private LineRenderer line;
    private void Start() 
    {   
        _forward = transform.forward;
        _ball = Camera.main.GetComponent<ModelTypeLoad>().ballTransform;
        line = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    }
    public void RotateUp()
    {
        StartCoroutine(Rotator());
    }

    IEnumerator Rotator()
    {   
        int k = 0;
        while(k < 40)
        {   
            Vector3 target = new Vector3(_ball.position.x + transform.right.x, transform.position.y, _ball.position.z + transform.right.z);
            //Vector3 target = _ball.position - transform.right;
            transform.LookAt(target);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, _ball.position);
            yield return new WaitForSeconds(0.05f);
            k++;
        }
        Destroy(line.gameObject);
    }
}
