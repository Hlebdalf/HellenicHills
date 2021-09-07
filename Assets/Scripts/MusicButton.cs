using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{   
    private bool _isMusic = true;
    public Audio audio;
    private void Start()
    {
        if (PlayerPrefs.GetInt("isMusic") == 1)
        {
            Play(true);
            _isMusic = true;
        }
        else 
        {
            Play(false);
             _isMusic = false;
        }
        ChangeColor();
    }

    public void Release()
    {
        _isMusic = !_isMusic;
        Play(_isMusic);
        ChangeColor();
    }
    private void Play(bool how)
    {
        audio.PlayMusic(how);
    }
    private void ChangeColor()
    {
        if (_isMusic)
        {
            GetComponent<Image>().color = new Color(0, 1, 0);
            PlayerPrefs.SetInt("isMusic", 1);
        }
        else 
        {
            GetComponent<Image>().color = new Color(1, 0, 0);
            PlayerPrefs.SetInt("isMusic", 0);
        }
    }
}
