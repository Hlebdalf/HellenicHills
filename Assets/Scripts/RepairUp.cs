using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUp : MonoBehaviour
{   
    private Transform _ball;    private Vector3 _forward;
    private LineRenderer line;
    private Vector2 _selfPos;
    private void Start() 
    {   
        _selfPos = new Vector2(transform.position.x, transform.position.z);
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
        for (int i =0; i < 80 * 5; i++)
        {   
            Vector2 _targetPos = new Vector2(_ball.position.x, _ball.position.z);
            float _angle = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), _targetPos - _selfPos);
            if(_angle > 180) _angle = -(360 - _angle);
            if(line != null)
            {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, _ball.position);
            }
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(0, _angle / 3, 0);
        }
        DestroyLine();       
    }

    public void DestroyLine()
    {   
        if(line != null)
        {
            StopCoroutine(Rotator());
            Destroy(line.gameObject);
        }
    }
}
