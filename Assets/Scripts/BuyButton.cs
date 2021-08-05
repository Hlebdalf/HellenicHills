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
    public GameObject[] buttons;
    private void Awake()
    {
        isBuyed = PlayerPrefs.GetInt(name + "button", 0) == 1;
    }
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = price.ToString();
        if (isBuyed)
        {   
            if(MagazineScroller.modelType == int.Parse(name)){
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
            content.GetComponent<MagazineScroller>().SaveModelType(name);
            gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0);
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "in use";
            IsUse();
        }

    }

    private void IsUse()
    {
        foreach(GameObject it in buttons)
        {
            if(it.GetComponent<BuyButton>().isBuyed && it.name != name)
            {
                it.GetComponent<Image>().color = new Color(1, 1, 0);
                it.transform.GetChild(0).GetComponent<Text>().text = "use?";
            }
        }
    }
    //DEBUG TOOL
    public void DeleteInfo()
    {
        isBuyed = false;
        PlayerPrefs.SetInt(name + "button", 0);
        PlayerPrefs.Save();
    }
    //DEBUG TOOL
}
