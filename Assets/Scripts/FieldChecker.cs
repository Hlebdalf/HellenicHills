using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FieldChecker : MonoBehaviour
{
    public Material coinUsedMaterial;
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
    public float fuelDecr = 2;
    public float healthDecr = 2;
    public float fuelIncr = 4;
    public float healthIncr = 4;
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
    private float _healthDecr;
    private float _fuelDecr;
    private float _healthIncr = 0;
    private float _fuelIncr = 0;
    public SphereCollider colliderTwo;


    private void Start()
    {
        upSnap.TransitionTo(0.3f);
        fuelBar.maxValue = fuel;
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckVolume());
        _fuelDecr = fuelDecr;
        _healthDecr = 0;
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
        colliderTwo.enabled = false;
        GetComponent<FieldChecker>().enabled = false;

    }
    public void PauseGame()
    {
        _isPause = !_isPause;
        if (_isPause)
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
            _rb.isKinematic = false;
            _rb.velocity = _preVelocity;
            canvas.GetComponent<Animator>().Play("ExitPause");
        }
    }
    public void GameStart()
    {   
        upSnap.TransitionTo(0.3f);
        VolumeSetActive();
        Mixer.audioMixer.SetFloat("MasterLowPass", 20000);
        StartCoroutine(Consumption());
        StartCoroutine(Restore());
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        _healthIncr = 0;
        _fuelIncr = 0;
        colliderTwo.enabled = true;
    }

    private void FieldObjEvent(string type, Collider other = null)
    {
        float damage = _rb.velocity.magnitude * mp;
        switch (type)
        {
            case "Chargers":
                break;
            case "Missions":
                Camera.main.GetComponent<Storytell>().UnlockStory();
                break;
            case "Ruble(Clone)":
                PartsCollect();
                other.enabled = false;
                other.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = coinUsedMaterial;
                break;
            case "Repairs":
                break;
            case "Water":
                break;
            case "Decorate":
                DamageMachine(damage);
                break;
            default:
                print(type);
                break;
        }
    }
    private void PartsCollect()
    {
        audio.CoinSound();
        partsAll++;
        partsAllText.text = "₽: " + partsAll.ToString();
        SaveParts();
    }
    private IEnumerator Consumption()
    {
        while (health > 1 && fuel > 1)
        {
            health -= _healthDecr;
            healthBar.value = health;
            fuel -= _fuelDecr;
            fuelBar.value = fuel;
            yield return new WaitForSeconds(0.1f);
        }
        GameOver();
    }

    private IEnumerator Restore()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.016f);
            if (fuel < 1000) fuel += _fuelIncr;
            else fuel = 1000;
            fuelBar.value = fuel;
            if (health < 1000) health += _healthIncr;
            else health = 1000;
            healthBar.value = health;
            fuelBar.value = fuel;
            healthBar.value = health;
        }
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
        if (health < 1) GameOver();
        else { _rb.isKinematic = false; }
    }

    private void VolumeSetActive()
    {
        if (_upOrDown)
        {
            FieldObjEvent("Water");
            volume.SetActive(false);
            panel.SetActive(false);
            _fuelDecr = fuelDecr;
            _healthDecr = 0;
        }
        else
        {
            FieldObjEvent("Water");
            volume.SetActive(true);
            panel.SetActive(true);
            _healthDecr = healthDecr;
            _fuelDecr = 0;
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
            FieldObjEvent(other.name, other);
            if (other.name == "Repairs")
            {
                other.GetComponent<InterFO>().transform.GetChild(0).GetChild(0).gameObject.GetComponent<RepairUp>().RotateUp();
                _healthIncr += healthIncr;
            }
            if (other.name == "Chargers")
            {
                _fuelIncr += fuelIncr;
                other.GetComponent<InterFO>().transform.GetChild(0).GetChild(0).gameObject.GetComponent<RepairUp>().RotateUp();
            }
        }
        // else{
        //     Debug.Log(other.gameObject.tag);
        // }
    }

    public void ChangeRepairChargeVelocity(string target)
    {   
        if (target == "Charger" ) _fuelIncr -= fuelIncr;
        else if (target == "Repair") _healthIncr -= healthIncr;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Decorate"))
        {
            FieldObjEvent("Decorate");
        }
    }
}
