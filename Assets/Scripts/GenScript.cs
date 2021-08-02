using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenScript : MonoBehaviour
{   
    public Vector2Int Resolution = new Vector2Int(0, 0);
    public int koeff = 1;
    public Material NoiseMaterial;
    public Material SpruceMaterial;
    public Material RefMaterial;
    public UIButtonManager canvas;
    public TerrainData[] datas = new TerrainData[8];
    public Material[] materials = new Material[8];
    public GameObject Ball;
    public GameObject Marker;
    public GameObject FO;
    public Texture2D[] HeightMaps = new Texture2D[2];
    public float Seed;
    private Vector2Int nowPos, prePos = new Vector2Int(0, 0);
    private Dictionary<Vector2Int, GameObject> terrains = new Dictionary<Vector2Int, GameObject>();
    private int dataID = 0;
    private bool isStarted = false;

    private void Awake()
    {
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        Ball.transform.position = new Vector3(1, 70, Resolution.y);
        Seed = Random.Range(-10000f, 10000f);
        StartCoroutine(BuildTerrain());
    }

    private void FOInit(GameObject target)
    {
        target.GetComponent<FieldObjMarker>().refMarker = Marker;
        target.GetComponent<FieldObjMarker>().canvas = canvas.gameObject;
        target.GetComponent<FieldObjMarker>().ball = Ball;
       // target.GetComponent<FieldObjScript>().death = gameObject.GetComponent<FieldChecker>();
        target.GetComponent<FieldObjMarker>().StartGame();
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
        Vector2Int[] allNbhs = new Vector2Int[6];
        List<Vector2Int> result = new List<Vector2Int>();
        allNbhs[0] = new Vector2Int(pos.x, pos.y);
        allNbhs[1] = new Vector2Int(pos.x, pos.y - 1);
        allNbhs[2] = new Vector2Int(pos.x, pos.y + 1);
        allNbhs[3] = new Vector2Int(pos.x + 1, pos.y);
        allNbhs[4] = new Vector2Int(pos.x + 1, pos.y - 1);
        allNbhs[5] = new Vector2Int(pos.x + 1, pos.y + 1);
        
        for (int i = 0; i < 6; i++)
        {
            if (!terrains.ContainsKey(allNbhs[i]))
            {
                result.Add(allNbhs[i]);
            }
        }
        return result.ToArray();
    }

    private void DestroyOldTerrains()
    {
        List<Vector2Int> keys = terrains.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            if (terrains[keys[i]].transform.position.x < Ball.transform.position.x - Resolution.y * koeff - 10)
            {
                Destroy(terrains[keys[i]]);
                terrains.Remove(keys[i]);
            }
        }
    }

    private void SpawnTerrains()
    {
        Vector2Int[] neighbours = GetNeighbours(nowPos);
        foreach (Vector2Int nb in neighbours)
        {
            GameObject newTerrain = Terrain.CreateTerrainGameObject(datas[dataID]);
            newTerrain.AddComponent(typeof(TerrainInit)); 
            newTerrain.GetComponent<TerrainInit>().InitTerrain(NoiseMaterial, SpruceMaterial, materials[dataID], Resolution, nb, Seed,koeff, FO);
            terrains.Add(nb, newTerrain);
            dataID = (dataID + 1) % 12;
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
                yield return null;
                DestroyOldTerrains();
            }
            yield return new WaitForSeconds(2);
            if(!isStarted && dataID > 4)
            {
                isStarted = true;
                StartGame();
            }
        }
    }
}


