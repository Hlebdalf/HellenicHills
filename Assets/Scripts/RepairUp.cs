using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUp : MonoBehaviour
{
    private Transform _ball; private Vector3 _forward;
    private LineRenderer line;
    private Vector2 _selfPos;
    private void Start()
    {
        _selfPos = new Vector2(transform.position.x, transform.position.z);
        _forward = transform.forward;
        _ball = Camera.main.transform.parent;
        line = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    }
    public void RotateUp()
    {
        StartCoroutine(Rotator());
    }

    private float SignedAngleBetween(Vector2 self, Vector2 target, Vector3 n)
    {
        Vector3 a = new Vector3(self.x, 0, self.y);
        Vector3 b = new Vector3(target.x, 0, target.y);
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        float signed_angle = angle * sign;
        return signed_angle;
    }

    IEnumerator Rotator()
    {
        while(true)
        {
            Vector2 _targetPos = new Vector2(_ball.position.x, _ball.position.z) - _selfPos;
            Vector2 _forward2 = new Vector2(transform.forward.x, transform.forward.z);
            float _angle = SignedAngleBetween(_forward2, _targetPos, new Vector3(0, 1, 0));
            if (line != null)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, _ball.position);
            }
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(0, _angle / 3, 0);
        }
    }

    public void DestroyLine()
    {
        if (line != null)
        {
            StopCoroutine(Rotator());
            Destroy(line.gameObject);
        }
    }
}
