using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{   
    public Vector3 normal;
    public void SetGrass(Terrain terrain)
    {   
        transform.LookAt(normal + transform.position - transform.right);
        transform.Rotate(0, Random.value * 360, 0);
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
                    transform.GetChild(i).Rotate(0, Random.value * 360, 0);
                }
            }
        }
    }

}
