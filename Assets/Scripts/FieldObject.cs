using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour
{
    private float _chargerChance = 20;
    private float _repairChance = 20;
    private float _decorateChanse = 1000;
    private float _missionChance = 2;
    private float _partChance = 8;
    private int _type;
    public Vector3 normal;
    private void Start()
    {
        float coin = Random.Range(0, _partChance + _chargerChance + _missionChance + _decorateChanse);
        if (coin < _missionChance) _type = 2;
        else if (coin < _missionChance + _partChance) _type = 3;
        else if (coin < _missionChance + _partChance + _chargerChance) _type = 0;
        else if (coin < _missionChance + _partChance + _chargerChance + _repairChance) _type = 4;
        else _type = 1;
        for (int i =0; i < transform.childCount; i++)
        {
            if (i == _type)
            {
                transform.GetChild(i).gameObject.SetActive(true); 
                continue;
            }
            Destroy(transform.GetChild(i).gameObject);
        }

        if (_type != 1)
        {
            GetComponent<FieldObjMarker>().StartGame(transform.GetChild(_type).gameObject.name);
        }
        else
        {
            GetComponent<FieldObjMarker>().enabled = false;
        }
    }
}
