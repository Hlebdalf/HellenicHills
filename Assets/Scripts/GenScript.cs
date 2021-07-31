using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenScript : MonoBehaviour
{
    public Vector2Int Resolution = new Vector2Int(0, 0);
    public Material NoiseMaterial;
    public Material SpruceMaterial;
    public UIButtonManager canvas;
    public GameObject Ball;
    public GameObject Marker;
    public GameObject RefTerrain;
    public Texture2D[] HeightMaps = new Texture2D[2];
    public float Seed;
    private Vector2Int nowPos, prePos = new Vector2Int(0, 0);
    private Vector2Int[] buzyCells = new Vector2Int[8];
    private GameObject[] buzyTerrains = new GameObject[8];
    private void Awake()
    {
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        Ball.transform.position = new Vector3(-1, 5000, Resolution.y);
        Seed = Random.Range(-10000f, 10000f);
        StartCoroutine(BuildTerrain());
    }

    private void FOInit(GameObject target)
    {
        target.GetComponent<FieldObjMarker>().refMarker = Marker;
        target.GetComponent<FieldObjMarker>().canvas = canvas.gameObject;
        target.GetComponent<FieldObjMarker>().ball = Ball;
        target.GetComponent<FieldObjScript>().death = gameObject.GetComponent<Death>();
        target.GetComponent<FieldObjMarker>().StartGame();
    }

    public Texture2D BakeTerrain(Vector2 offset)
    {
        NoiseMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", offset.x + Seed);
        NoiseMaterial.SetFloat("Vector1_fd0d843ba4ac45c2bd344a013bfa0ab7", offset.y);
        NoiseMaterial.SetFloat("Vector1_090150e04f634e6eb9f7220d01725be0", Seed);
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.y + 1, Resolution.y + 1);
        Graphics.Blit(null, renderTexture, NoiseMaterial);
        Texture2D texture = new Texture2D(Resolution.y + 1, Resolution.y + 1);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.y + 1, Resolution.y + 1)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture;
    }
    public Texture2D BakeFO()
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x / 2, Resolution.y / 2);
        Graphics.Blit(null, renderTexture, SpruceMaterial);
        Texture2D texture = new Texture2D(Resolution.x / 2, Resolution.y / 2);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.x / 2, Resolution.y / 2)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture;
    }


    private void PutFO(GameObject self)
    {
        RaycastHit hit;
        Ray ray = new Ray(self.transform.position, new Vector3(0, -300, 0));
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Terrain") self.transform.position = hit.point;
            else DestroyImmediate(self, true);
        }
        else DestroyImmediate(self, true);

    }
    private Vector2Int[] GetNeighbours(Vector2Int pos)
    {
        Vector2Int[] result = new Vector2Int[5];
        result[0] = new Vector2Int(pos.x, pos.y - 1);
        result[1] = new Vector2Int(pos.x, pos.y + 1);
        result[2] = new Vector2Int(pos.x + 1, pos.y);
        result[3] = new Vector2Int(pos.x + 1, pos.y - 1);
        result[4] = new Vector2Int(pos.x + 1, pos.y + 1);
        return result;
    }

    private void RestructBuzyObjects(Vector2Int bc, GameObject terrain)
    {
        if()
    }

    private void SpawnTerrains()
    {
        Vector2Int[] allNeighbours = GetNeighbours(nowPos);
        for (int i = 0; i < 5; i++)
        {
            Vector2Int nb = allNeighbours[i];
            foreach (Vector2Int bc in buzyCells)
            {
                if (nb == bc) allNeighbours[i].x = -1; // -1 means that this cell is not suitable, because -1 is not reached for x
            }
        }
        foreach (Vector2Int nb in allNeighbours)
        {
            if (nb.x != -1)
            {
                GameObject newTerrain = Instantiate(RefTerrain);
                newTerrain.transform.position = new Vector3((nb.x) * Resolution.y, 0, (nb.y) * Resolution.y);
                RestructBuzyObjects(nb, newTerrain);
            }
        }
    }

    private void StartGame()
    {
        canvas.MenuUIActive();
    }
    IEnumerator BuildTerrain()
    {
        while (true)
        {

            nowPos = new Vector2Int((int)Mathf.Floor(Ball.transform.position.x / Resolution.y), (int)Mathf.Floor(Ball.transform.position.z / Resolution.y));
            if (nowPos != prePos)
            {
                SpawnTerrains();
            }
            yield return new WaitForSeconds(2);
        }
    }
}


