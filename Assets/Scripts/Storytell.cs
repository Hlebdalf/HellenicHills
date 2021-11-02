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
        progress = PlayerPrefs.GetInt("progress", 0);
        for (int i = 0; i < progress; i++)
        {
            fullStoryText.text = fullStoryText.text + stories[i] + '\n';
        }
}
    public void UnlockStory()
    {     
        if (progress >= stories.Length) progress -= 1;
        StartCoroutine(ShowText(stories[progress]));
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
