using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{   
    private bool _isSound = true;
    public Audio audio;
    private void Start()
    {
        if (PlayerPrefs.GetInt("isSound") == 1)
        {
            Play(true);
            _isSound = true;
        }
        else 
        {
            Play(false);
             _isSound = false;
        }
        ChangeColor();
    }

    public void Release()
    {
        _isSound = !_isSound;
        Play(_isSound);
        ChangeColor();
    }
    private void Play(bool how)
    {
        audio.PlaySound(how);
    }
    private void ChangeColor()
    {
        if (_isSound)
        {
            GetComponent<Image>().color = new Color(0, 1, 0);
            PlayerPrefs.SetInt("isSound", 1);
        }
        else 
        {
            GetComponent<Image>().color = new Color(1, 0, 0);
            PlayerPrefs.SetInt("isSound", 0);
        }
    }
}
