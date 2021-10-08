using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTypeLoad : MonoBehaviour
{
    public GameObject ups;
    public GameObject balls;
    public Transform ballTransform;
    private int type;
    void Awake()
    {
        type = PlayerPrefs.GetInt("modelType");
        ups.transform.localPosition = new Vector3(-type * 5, 0, type * 5);
        balls.transform.localPosition = new Vector3(-type * 5, 0, type * 5);
        for (int i = 0; i < ups.transform.childCount; i++)
        {
            if (i == type)
            {   
                ballTransform = balls.transform.GetChild(i).gameObject.transform;
                continue;
            }
            ups.transform.GetChild(i).gameObject.SetActive(false);
            balls.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
