using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FieldChecker : MonoBehaviour
{   
    public GameObject volume;
    public GameObject panel;
    public bool _upOrDown = true;
    public float fuel = 1000;
    public float consumption = 2;
    public Slider fuelBar;
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
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckVolume());
    }

    private void Awake()
    {   
        scoreRecordText.text = PlayerPrefs.GetInt("scoreRecord").ToString(); 
        partsAll = PlayerPrefs.GetInt("partsAll");
        partsAllText.text = partsAll.ToString();
    }
    public void GameOver()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        PlayerPrefs.SetInt("partsAll", partsAll);
        PlayerPrefs.Save();   
        reloadButton.SetActive(true);
    }
    public void GameStart()
    {
        StartCoroutine(FuelConsumption());
    }

    private void FieldObjEvent(string type)
    {   
        IsConsumption(false);
        float damage = _rb.velocity.magnitude * mp;
        switch (type) {  
            case "Chargers":
                StartCoroutine(ChargeCoroutine());
                break;
            case "Missions":
                print("Mission");
                GetComponent<Rigidbody>().isKinematic = false;
                break;
            case "Parts":
                StartCoroutine(PartsCollectCoroutine());
                break;
            case "Repairs":
                StartCoroutine(RepairCoroutine());
                break;
            case "Water":
                IsConsumption(true);
                break;
            case "Decorate":
                IsConsumption(true);
                DamageMachine(damage);
                break;
            default:
                print(type);
                break;
        }
    }
    private IEnumerator PartsCollectCoroutine()
    {   
        _rb.isKinematic = true;
        yield return new WaitForSeconds(2);
        partsAll += (int)Random.Range(0, 10.0f);
        partsAllText.text = partsAll.ToString();
        GetComponent<Rigidbody>().isKinematic = false;
        _rb.isKinematic = false;
        IsConsumption(true);
    }
    private IEnumerator FuelConsumption()
    {
        while (fuel > 1)
        {
            fuel -= consumption;
            fuelBar.value = fuel;
            yield return new WaitForSeconds(0.1f);
        }
        GameOver();

    }
    
    private IEnumerator HealthConsumption()
    {   
        while (health > 1)
        {
            health -= consumption * mp;
            healthBar.value = health;
            yield return new WaitForSeconds(0.1f);
        }
        GameOver();
    }

    private IEnumerator ChargeCoroutine()
    {   
        _rb.isKinematic = true;
        yield return new WaitForSeconds(2);
        fuel = 1000;
        fuelBar.value = fuel;
        GetComponent<Rigidbody>().isKinematic = false;
        _rb.isKinematic = false;
        IsConsumption(true);
    }
    
    private IEnumerator RepairCoroutine()
    {   
        _rb.isKinematic = true;
        yield return new WaitForSeconds(2);
        health = 1000;
        healthBar.value = health;
        GetComponent<Rigidbody>().isKinematic = false;
        _rb.isKinematic = false;
        IsConsumption(true);
    }
    
    private IEnumerator CheckVolume()
    {
        while (true)
        {
            if (transform.position.y < 32)
            {
                if (_upOrDown)
                {   
                    _upOrDown = false;
                    VolumeSetActive();
                }
                _upOrDown = false;
            }
            else
            {
                if (!_upOrDown)
                {
                    _upOrDown = true;
                    VolumeSetActive();
                }
                _upOrDown = true;
            }
            yield return new WaitForSeconds(0.066f);
        }
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
        _rb.isKinematic = false;
    }

    private void IsConsumption(bool how)
    {
        if (how)
        {
            if (_upOrDown)
            {
                StartCoroutine(FuelConsumption());
                StopCoroutine(HealthConsumption());
            }
            else
            {
                StartCoroutine(HealthConsumption());
                StopCoroutine(FuelConsumption());
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(CheckVolume());
        }
    }
    
    private void VolumeSetActive()
    {
        if (_upOrDown)
        {
            FieldObjEvent("Water");
            volume.SetActive(false); 
            panel.SetActive(false);
        }
        else
        {
            FieldObjEvent("Water");
            volume.SetActive(true);
            panel.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Decorate"))
        {
            FieldObjEvent("Decorate");
        }
        else if (other.CompareTag("Interactive"))
        {
            FieldObjEvent(other.name);
            other.GetComponent<BoxCollider>().enabled =  false;
        }
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

        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     for (int i = 0; i < transform.childCount; i++)
        //     {
        //         transform.GetChild(i).GetChild(0).gameObject.GetComponent<BuyButton>().DeleteInfo();
        //     }
        // }
        //DEBUG TOOL
    }
}
