using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public GameObject target;
    public Text scoresText;
    public int scores;
    private int scoreRecord = 0;
    private void Awake()
    {
        scoreRecord = PlayerPrefs.GetInt("scoreRecord");
    }
    void Update()
    {
        gameObject.transform.position = target.transform.position;
        if (gameObject.transform.position.x > scores)
        {
            scores = (int)gameObject.transform.position.x;
            scoresText.text = "m: " + scores.ToString() + "/" + scoreRecord.ToString();
            if (scores > scoreRecord)
            {
                PlayerPrefs.SetInt("scoreRecord", scores);
            }
        }
        //DEBUG TOOL
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("scoreRecord", 0);
        }
        //DEBUG TOOL
    }
}
