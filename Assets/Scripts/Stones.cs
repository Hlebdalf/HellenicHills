using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : MonoBehaviour
{
    private Vector3 up;
    void Start()
    {
        up = transform.up;
        float childsCNT = (float)transform.childCount;
        float coin = Random.value;

        int target = Mathf.FloorToInt(coin * childsCNT * childsCNT / childsCNT);
        for (int i = 0; i < childsCNT; i++)
        {
            if (i == target) continue;
            else Destroy(transform.GetChild(i).gameObject);
        }
        transform.LookAt(transform.parent.parent.GetComponent<FieldObject>().normal + transform.position- up);
    }
}
