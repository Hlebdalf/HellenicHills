using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    public GameObject ReloadButton;
    public void ReloadScene()
    {
        ReloadButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
