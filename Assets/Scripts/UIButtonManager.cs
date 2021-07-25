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
        MenuUI.GetComponent<TouchToPlay>().enabled = false;
        Magazine.SetActive(true);
        Stats.SetActive(false);
        Camera.main.GetComponent<Animator>().Play("Forward");
        StartCoroutine(ChoiseActive());
    }
    public void MagazineSetDisactive()
    {
        MenuUI.GetComponent<TouchToPlay>().enabled = true;
        Content.GetComponent<Image>().raycastTarget = false;
        Content.GetComponent<MagazineScroller>().ModelSwitcher();
        Camera.main.GetComponent<Animator>().Play("Back");
        Magazine.SetActive(false);
        Stats.SetActive(true);
    }
    IEnumerator ChoiseActive()
    {
        yield return new WaitForSeconds(1);
        Content.GetComponent<MagazineScroller>().ChoiseActive();
        Content.GetComponent<Image>().raycastTarget = true;
    }
}
