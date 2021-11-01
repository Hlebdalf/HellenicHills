using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public void SetGrass(Terrain terrain)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.GetChild(i).position, -Vector3.up) ;
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject != terrain.gameObject)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                else
                {
                    transform.GetChild(i).position = hit.point;
                }
            }
        }
    }

}
