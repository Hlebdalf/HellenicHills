using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public float fuel = 1000;
    public float consumption = 2;
    public Slider fuelBar;
    public GameObject Ball;
    public GameObject Ball_up;
    public GameObject ReloadButton;
    public Text scoreRecordText;
    public Text partsAllText;
    public int partsAll = 0; //

    private void Start()
    {
        fuelBar.maxValue = fuel;
    }

    private void Awake()
    {
        scoreRecordText.text = PlayerPrefs.GetInt("scoreRecord").ToString();
        partsAll = PlayerPrefs.GetInt("partsAll");
        partsAllText.text = partsAll.ToString();
    }

    public void GameOver(){

    Ball.GetComponent<Rigidbody>().isKinematic = true;
    PlayerPrefs.SetInt("partsAll", partsAll);
    PlayerPrefs.Save();
    ReloadButton.SetActive(true);
}

public void GameStart()
    {
        StartCoroutine(FuelConsumption());
    }

    public void FieldObjEvent(string type)
    {
        Ball.GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        switch (type) {  
            case "Charger(Clone)":
                StartCoroutine(ChargeCoroutine());
                break;
            case "Mission(Clone)":
                print("Mission");
                Ball.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(FuelConsumption());
                break;
            case "Parts(Clone)":
                StartCoroutine(PartsCollectCoroutine());
                break;
            default:
                print(type);
                break;
        }
    }
    public IEnumerator PartsCollectCoroutine()
    {
        yield return new WaitForSeconds(2);
        partsAll += (int)Random.Range(0, 10.0f);
        partsAllText.text = partsAll.ToString();
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(FuelConsumption());
    }
    public IEnumerator FuelConsumption()
    {
        while (fuel > 1)
        {
            fuel -= consumption;
            fuelBar.value = fuel;
            yield return new WaitForSeconds(0.1f);
        }
        GameOver();

    }

    public IEnumerator ChargeCoroutine()
    {
        yield return new WaitForSeconds(2);
        fuel = 1000;
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(FuelConsumption());
    }

    public void SaveParts()
    {
        partsAllText.text = partsAll.ToString();
        PlayerPrefs.SetInt("partsAll", partsAll);
        PlayerPrefs.Save();
    }

    void Update()
    {
        //DEBUG TOOL
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            partsAll = 1000;
            partsAllText.text = partsAll.ToString();
            PlayerPrefs.SetInt("partsAll", partsAll);
            PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            partsAll = 0;
            partsAllText.text = partsAll.ToString();
            PlayerPrefs.SetInt("partsAll", partsAll);
            PlayerPrefs.Save();
        }
        //DEBUG TOOL
    }

