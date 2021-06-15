using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject target;
    public Text scoresText;
    public int scores;
    void Update()
    {
        gameObject.transform.position = target.transform.position;
        if(gameObject.transform.position.x > scores)
        {
            scores = (int)gameObject.transform.position.x;
            scoresText.text = scores.ToString();
        }
    }
}
