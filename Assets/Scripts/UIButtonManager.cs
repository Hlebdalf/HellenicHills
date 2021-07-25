using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    public GameObject Ball;
    public GameObject ReloadButton;
    public GameObject MenuUI;
    public GameObject InGameUI;
    public GameObject myCamera;
    public GameObject Magazine;
    public GameObject Stats;
    
    public void ReloadScene()
    {
        ReloadButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartSession()
    {
        InGameUI.SetActive(true);
        MenuUI.SetActive(false);
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        myCamera.GetComponent<Death>().GameStart();
    }

    public void MenuUIActive()
    {
        MenuUI.SetActive(true);
    }
    public void MagazineSetActive()
    {
        Magazine.SetActive(true);
        Stats.SetActive(false);
        Camera.main.GetComponent<Animator>().Play("Forward");
    }
    public void MagazineSetDisactive()
    {
        Camera.main.GetComponent<Animator>().Play("Back");
        Magazine.SetActive(false);
        Stats.SetActive(true);
    }
}
