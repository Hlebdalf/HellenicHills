using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject Content;
    public GameObject PartsAll;
    public TouchToPlay ttp;

    public void ReloadScene()
    {
        ReloadButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartSession()
    {
        InGameUI.SetActive(true);
        GetComponent<Animator>().Play("GameStart");
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        Ball.GetComponent<FieldChecker>().GameStart();
    }

    public void MenuUIActive()
    {
        MenuUI.SetActive(true);
        PartsAll.SetActive(true);
    }
    public void MagazineSetActive()
    {
        ttp.enabled = false;
        Magazine.SetActive(true);
        Stats.SetActive(false);
        Camera.main.GetComponent<Animator>().Play("Forward");
        Content.GetComponent<Magazine>().RefreshButtons();
        StartCoroutine(ChoiseActive());
    }

    public void EnterSettings()
    {
        GetComponent<Animator>().Play("EnterSettings");
    }
    public void EnterStory()
    {
        GetComponent<Animator>().Play("EnterStory");
    }
    public void ExitStory()
    {
        GetComponent<Animator>().Play("ExitStory");
    }
    public void ExitSettings()
    {
        GetComponent<Animator>().Play("ExitSettings");
    }
    public void MagazineSetDisactive()
    {
        ttp.enabled = true;
        Content.GetComponent<Image>().raycastTarget = false;
        Content.GetComponent<Magazine>().ModelSwitcher();
        Camera.main.GetComponent<Animator>().Play("Back");
        Magazine.SetActive(false);
        Stats.SetActive(true);
    }
    IEnumerator ChoiseActive()
    {
        yield return new WaitForSeconds(1);
        Content.GetComponent<Magazine>().ChoiseActive();
        Content.GetComponent<Image>().raycastTarget = true;
    }
}
