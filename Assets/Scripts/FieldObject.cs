using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour
{
    private float ChargerChance = 40;
    private float DecorateChanse = 1000;
    private float MissionChance = 2;
    private float PartChance = 8;
    private int type;
    public Vector3 normal;
    void Start()
    {   

        float coin = Random.Range(0, PartChance + ChargerChance + MissionChance + DecorateChanse);
        if (coin < MissionChance) type = 2;
        else if (coin < MissionChance + PartChance) type = 3;
        else if (coin < MissionChance + PartChance + ChargerChance) type = 0;
        else type = 1;
        for (int i =0; i < transform.childCount; i++)
        {
            if (i == type)
            {
                transform.GetChild(i).gameObject.SetActive(true); 
                continue;
            }
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
