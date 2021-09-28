using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FieldChecker : MonoBehaviour
{   
    public Sprite resume;
    public Sprite pause;
    public Image pauseButton;
    public Audio audio;
    public AudioMixerGroup Mixer;    
    public AudioMixerSnapshot upSnap;
    public AudioMixerSnapshot downSnap;
    public AudioMixerSnapshot gameOver;
    public GameObject canvas;
    public GameObject magazine;
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
    private bool _isPause = false;
    private Vector3 _preVelocity = new Vector3(0, 0, 0);
    

    private void Start()
    {    
        upSnap.TransitionTo(0.3f);
        fuelBar.maxValue = fuel;
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckVolume());
    }

    private void Awake()
    {   
        scoreRecordText.text = PlayerPrefs.GetInt("scoreRecord").ToString(); 
        partsAll = PlayerPrefs.GetInt("partsAll");
        partsAllText.text = "₽: " + partsAll.ToString();
    }
    public void GameOver()
    
    {   
        audio.PlayMusic(false);
        gameOver.TransitionTo(0.1f);
        StopAllCoroutines();
        GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody>().isKinematic = true;
        SaveParts();
        canvas.GetComponent<Animator>().Play("GameOver");
        GetComponent<FieldChecker>().enabled = false;
    }
    public void PauseGame()
    {   
        _isPause = !_isPause;
        if(_isPause)
        {   
            _preVelocity = _rb.velocity;
            StopAllCoroutines();
            _rb.isKinematic = true;
            pauseButton.sprite = resume;
            canvas.GetComponent<Animator>().Play("EnterPause");
        } 
        else 
        {   
            pauseButton.sprite = pause;
            IsConsumption(true);
            _rb.isKinematic = false;
            _rb.velocity = _preVelocity;
            canvas.GetComponent<Animator>().Play("ExitPause");
        }
    }
    public void GameStart()
    {
        StartCoroutine(FuelConsumption());
        transform.GetChild(0).GetComponent<AudioSource>().Play();
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
        partsAllText.text = "₽: " + partsAll.ToString();
        SaveParts();
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
            health -= (consumption * mp);
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
        //_rb.isKinematic = true;
        yield return new WaitForSeconds(2);
        health = 1000;
        healthBar.value = health;
        GetComponent<Rigidbody>().isKinematic = false;
        _rb.isKinematic = false;
        IsConsumption(true);
    }
    
    private IEnumerator CheckVolume()
    {
        while (health > 1 && fuel > 1)
        {
            if (transform.position.y < 32)
            {
                if (_upOrDown)
                {   
                    downSnap.TransitionTo(0.3f);
                    _upOrDown = false;
                    VolumeSetActive();
                    Mixer.audioMixer.SetFloat("MasterLowPass", 600);
                }
                _upOrDown = false;
            }
            else
            {
                if (!_upOrDown)
                {
                    _upOrDown = true;
                    upSnap.TransitionTo(0.3f);
                    VolumeSetActive();
                    Mixer.audioMixer.SetFloat("MasterLowPass", 20000);
                }
                _upOrDown = true;
            }
            
            yield return new WaitForFixedUpdate();
        }
    }

    public void SaveParts()
    {
        partsAllText.text = "₽: " + partsAll.ToString();
        PlayerPrefs.SetInt("partsAll", partsAll);
        PlayerPrefs.Save();
    }

    private void DamageMachine(float damage)
    {
        audio.HitSound(_rb.velocity.x / 25);
        health -= damage * mp;
        healthBar.value = health;
        if(health < 1) GameOver();
        else{_rb.isKinematic = false;}
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
            if(other.GetComponent<Repairs>())
            {
                other.GetComponent<Repairs>().transform.GetChild(0).GetChild(0).gameObject.GetComponent<RepairUp>().RotateUp();
            }
        }
    }
    void Update()
    {
        //DEBUG TOOL
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            partsAll = 1000;
            partsAllText.text = "₽: " + partsAll.ToString();
            PlayerPrefs.SetInt("partsAll", partsAll);
            PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            partsAll = 0;
            partsAllText.text = "₽: " + partsAll.ToString();
            PlayerPrefs.SetInt("partsAll", partsAll);
            PlayerPrefs.Save();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                magazine.GetComponent<Magazine>().DeleteButtons();
            }
        }
        //DEBUG TOOL
    }
}
