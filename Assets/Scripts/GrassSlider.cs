using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassSlider : MonoBehaviour
{
    public Text comment;
    private Slider _slider;
    public string[] comments; 
    private void Start()
    {   
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat("GrassTreshold", 0);
    }
    public void OnValueChange()
    {
        comment.text = comments[(int)(_slider.value * (comments.Length-1))];
        comment.color = new Color (1, 1 - _slider.value, 1 - _slider.value);
        Camera.main.GetComponent<GenScript>().SetGrassTreshold(_slider.value);
        PlayerPrefs.SetFloat("GrassTreshold", _slider.value);
        PlayerPrefs.Save();
    }
}
