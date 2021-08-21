using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FieldChecker : MonoBehaviour
{
    public float fuel = 1000;
    public float consumption = 2;
    public Slider fuelBar;
    [FormerlySerializedAs("Ball")] public GameObject ball;
    [FormerlySerializedAs("Ball_up")] public GameObject ballUp;
    [FormerlySerializedAs("ReloadButton")] public GameObject reloadButton;
    public Text scoreRecordText;
    public Text partsAllText;
    public int partsAll = 0; 
    
    public float health = 1000;
    public Slider healthBar;
    public float mp = 1;
    private Rigidbody _rb;
    

    private void Start()
    {    
        fuelBar.maxValue = fuel;
        _rb = ball.GetComponent<Rigidbody>();
    }

    private void Awake()
    {   
        scoreRecordText.text = PlayerPrefs.GetInt("scoreRecord").ToString(); 
        partsAll = PlayerPrefs.GetInt("partsAll");
        partsAllText.text = partsAll.ToString();
    }
    public void GameOver()
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
        PlayerPrefs.SetInt("partsAll", partsAll);
        PlayerPrefs.Save();   
        reloadButton.SetActive(true);
    }
    public void GameStart()
    {
        StartCoroutine(FuelConsumption());
    }

    public void FieldObjEvent(string type)
    {   
        float damage = _rb.velocity.magnitude * mp;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        switch (type) {  
            case "Chargers":
                StartCoroutine(ChargeCoroutine());
                break;
            case "Mission(Clone)":
                print("Mission");
                ball.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(FuelConsumption());
                break;
            case "Parts(Clone)":
                StartCoroutine(PartsCollectCoroutine());
                break;
            case "Repairs":
                StartCoroutine(RepairCoroutine());
                break;
            case "EnterWater":
                StartCoroutine(HealthConsumption());
                break;
            case "ExitWater":
                StopCoroutine(HealthConsumption());
                break;
            case "Decorates":
                DamageMachine(damage);
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
        ball.GetComponent<Rigidbody>().isKinematic = false;
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
    
    public IEnumerator HealthConsumption()
    {   
        _rb.isKinematic = false;
        while (health > 1)
        {
            health -= consumption * mp;
            healthBar.value = health;
            yield return new WaitForSeconds(0.1f);
        }
        GameOver();
    }

    public IEnumerator ChargeCoroutine()
    {
        yield return new WaitForSeconds(2);
        fuel = 1000;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(FuelConsumption());
    }
    
    public IEnumerator RepairCoroutine()
    {
        yield return new WaitForSeconds(2);
        health = 1000;
        healthBar.value = health;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(FuelConsumption());
    }

    public void SaveParts()
    {
        partsAllText.text = partsAll.ToString();
        PlayerPrefs.SetInt("partsAll", partsAll);
        PlayerPrefs.Save();
    }

    private void DamageMachine(float damage)
    {
        
        health -= damage * mp;
        healthBar.value = health;
        if(health <= 0) GameOver();
        StartCoroutine(FuelConsumption());
        _rb.isKinematic = false;
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
}
