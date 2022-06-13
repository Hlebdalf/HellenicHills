using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour
{
    public Vector2Int vc = new Vector2Int(0, 0);

    private void Start()
    {
        Application.targetFrameRate = 60;
        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             GameObject go = Instantiate(new GameObject(), new Vector3(0,0,0), Quaternion.identity);
             go.AddComponent<Chunk>();
         }
         if (Input.GetKeyDown(KeyCode.Backspace))
         {
             GameObject go = Instantiate(new GameObject(), new Vector3(64, 0, 0), Quaternion.identity);
             go.AddComponent<Chunk>();
         }
         if (Input.GetKeyDown(KeyCode.F))
         {
             GameObject go = Instantiate(new GameObject(), new Vector3(64, 0, 64), Quaternion.identity);
             go.AddComponent<Chunk>();
         }
         if (Input.GetKeyDown(KeyCode.G))
         {
             GameObject go = Instantiate(new GameObject(), new Vector3(0, 0, 64), Quaternion.identity);
             go.AddComponent<Chunk>();
         }*/
        for (int i = 0; i < 10; i++)
        {
            for (int j =0; j < 10; j++)
            {
                GameObject go = Instantiate(new GameObject(), new Vector3(64 * i, 0, 64 * j), Quaternion.identity);
                go.AddComponent<Chunk>();
            }
        }
    }
}
