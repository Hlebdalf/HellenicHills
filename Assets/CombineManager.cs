using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CombineMeshe(List<CombineInstance> objects)
    {
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(objects.ToArray());
        transform.gameObject.SetActive(true);
    }
}
