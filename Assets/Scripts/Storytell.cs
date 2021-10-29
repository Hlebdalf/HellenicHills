using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Storytell : MonoBehaviour
{
    public string[] stories;
    public Text storyText;
    private int progress = 0;


    public void UnlockStory()
    {
        if (progress % 2 == 0)
        {
            StartCoroutine(ShowText(stories[(int)progress / 2 + progress % 2]));
        }
        progress++;
    }

    private IEnumerator ShowText(string text)
    {
        string str = "";
        int len = text.Length;
        for (int i = 0; i < len; i++)
        {
            str += text[i];
            storyText.text = str;
            yield return new WaitForSeconds(7 / len);
        }

        yield return new WaitForSeconds(5);
        storyText.text = "";
    }
}
