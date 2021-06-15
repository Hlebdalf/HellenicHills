using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        gameObject.transform.position = target.transform.position;
    }
}
