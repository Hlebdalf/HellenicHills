using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Storytell : MonoBehaviour
{   
    public float showSpeed = 0.03f;
    public string[] stories;
    public Text storyText;
    public Text fullStoryText;
    private int progress;
    public AudioSource typing;

    private void Start()
    {
/*      PlayerPrefs.SetInt("progress", 0);
        PlayerPrefs.Save();*/
        progress = PlayerPrefs.GetInt("progress", 0);
        for (int i = 0; i < progress/2; i++)
        {
            fullStoryText.text = fullStoryText.text + stories[i] + '\n';
        }
}
    public void UnlockStory()
    {     
        if (progress >= stories.Length * 2) progress -= 1;
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
        typing.Play();
        string str = "";
        int len = text.Length;
        for (int i = 0; i < len; i++)
        {
            str += text[i];
            storyText.text = str;
            if(text[i] == ' ')
            {
                typing.Stop();
                yield return new WaitForSeconds(showSpeed*2);
                typing.Play();
            }
            else yield return new WaitForSeconds(showSpeed);
        }
        typing.Stop();
        yield return new WaitForSeconds(5);
        storyText.text = "";
    }
}
