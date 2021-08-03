using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorates : MonoBehaviour
{
    void Start()
    {
        float childsCNT = (float)transform.childCount;
        float coin = Random.value;

        int target = Mathf.FloorToInt(coin * childsCNT * childsCNT / childsCNT);
        for (int i = 0; i < childsCNT; i++)
        {
            if (i == target) continue;
            else Destroy(transform.GetChild(i).gameObject);
        }
    }
}
