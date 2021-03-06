using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    void Start()
    {
        if (transform.position.y < 32)
        {
            Destroy(transform.parent.parent.gameObject);
        }
        float childsCNT = (float)transform.childCount;
        float coin = Random.value;

        int target = Mathf.FloorToInt(coin * childsCNT * childsCNT / childsCNT);
        for (int i = 0; i < childsCNT; i++)
        {
            if (i == target) continue;
            else Destroy(transform.GetChild(i).gameObject);
        }
        if (target != 0) transform.Rotate(0, coin * 360, 0);
        transform.localScale = new Vector3(coin / 2, coin / 2, coin / 2) + transform.localScale;

    }

}
