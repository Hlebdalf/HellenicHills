using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{
    public FieldChecker death;
    public GameObject content;
    public int price = 0;
    public bool isBuyed;
    private void Awake()
    {
        isBuyed = PlayerPrefs.GetInt(name + "button", 0) == 1;
    }
    private void Start(){
        Refresh();
    }
    public void Refresh()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = price.ToString();
        if (isBuyed)
        {   
            if(Magazine.modelType == int.Parse(name)){
                gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0);
                gameObject.transform.GetChild(0).GetComponent<Text>().text = "in use";
            }
            else {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 0);
                gameObject.transform.GetChild(0).GetComponent<Text>().text = "use?";
            }
        }
        else
        {
            if (death.partsAll < price)
            {
                gameObject.GetComponent<Button>().interactable = false;
                gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
            }
            else
            {
                gameObject.GetComponent<Button>().interactable = true;
                gameObject.GetComponent<Image>().color = new Color(0, 1, 0);

            }
        }
    }

    public void OnRelease()
    {
        if (!isBuyed && death.partsAll> price)
        {
            isBuyed = true;
            PlayerPrefs.SetInt(name + "button", 1);
            PlayerPrefs.Save();
            gameObject.GetComponent<Image>().color = new Color(1, 1, 0);
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "use?";
            death.partsAll -= price;
            death.SaveParts();
        }
        else if (isBuyed)
        {
            content.GetComponent<Magazine>().SaveModelType(name);
            gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0);
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "in use";
            content.GetComponent<Magazine>().RefreshButtons();
        }

    }

    public void DeleteInfo()
    {
        isBuyed = false;
        PlayerPrefs.SetInt(name + "button", 0);
        PlayerPrefs.Save();
    }
    //DEBUG TOOL
}
