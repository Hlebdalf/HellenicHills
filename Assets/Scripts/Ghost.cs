using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject target;
<<<<<<< HEAD
    void Update()
    {
        gameObject.transform.position = target.transform.position;
=======
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
            scoresText.text = scores.ToString() + "/" + scoreRecord.ToString();
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
>>>>>>> develop
    }
}
