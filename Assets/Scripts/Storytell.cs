using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Storytell : MonoBehaviour
{
    public float showSpeed = 0.03f;
    public string[] stories;
    public Text storyText;
    private int progress;

    private void Start()
    {
        progress = PlayerPrefs.GetInt("progress", 0);
    }
    public void UnlockStory()
    {     
        if (progress > stories.Length) progress = 0;
        if (progress % 2 == 0)
        {
            StartCoroutine(ShowText(stories[(int)progress / 2 + progress % 2]));
        }
        progress++;
        PlayerPrefs.SetInt("progress", progress);
        PlayerPrefs.Save();
    }

    private IEnumerator ShowText(string text)
    {
        string str = "";
        int len = text.Length;
        for (int i = 0; i < len; i++)
        {
            str += text[i];
            storyText.text = str;
            yield return new WaitForSeconds(showSpeed);
        }

        yield return new WaitForSeconds(5);
        storyText.text = "";
    }
}
