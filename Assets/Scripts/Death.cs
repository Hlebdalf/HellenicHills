using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public int fuel = 1000;
    public int consumption = 2;
    public Slider fuelBar;
    public GameObject Ball;
    public GameObject Ball_up;

    private void Start()
    {    
        fuelBar.maxValue = fuel;
    }

    public void GameOver()
    {
        Ball.GetComponent<Rigidbody>().isKinematic = true;
        Ball_up.GetComponent<Animator>().enabled = false;

        Debug.Log("Kek");
    }
    public void GameStart()
    {
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
        yield break;
    }
}
