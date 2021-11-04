using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class GenScript : MonoBehaviour
{
    public GameObject grass;
    public GameObject ruble;
    public float grassTreshold;
    [FormerlySerializedAs("Resolution")] public Vector2Int resolution = new Vector2Int(512, 512);
    public int koeff = 1;
    [FormerlySerializedAs("NoiseMaterial")] public Material noiseMaterial;
    [FormerlySerializedAs("SpruceMaterial")] public Material spruceMaterial;
    [FormerlySerializedAs("RefMaterial")] public Material refMaterial;
    public UIButtonManager canvas;
    public TerrainData[] datas = new TerrainData[8];
    public Material[] materials = new Material[8];
    [FormerlySerializedAs("Ball")] public GameObject ball;
    [FormerlySerializedAs("Marker")] public GameObject marker;
    [FormerlySerializedAs("FO")] public GameObject fo;
    public GameObject water;
    [FormerlySerializedAs("HeightMaps")] public Texture2D[] heightMaps = new Texture2D[2];
    [FormerlySerializedAs("Seed")] public float seed;
    private Vector2Int _nowPos, _prePos = new Vector2Int(0, 0);
    public Dictionary<Vector2Int, GameObject> terrains = new Dictionary<Vector2Int, GameObject>();
    private int _dataID = 0;
    private bool _isStarted = false;

    private void Awake()
    {
        SetGrassTreshold(PlayerPrefs.GetFloat("GrassTreshold", 0));
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
        gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        ball.transform.position = new Vector3(30, 70, resolution.y);
        seed = Random.Range(-10000f, 10000f);
        StartCoroutine(BuildTerrain());
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
            if (terrains[keys[i]].transform.position.x < ball.transform.position.x - resolution.y * koeff - 10)
            {
                DestroyImmediate(terrains[keys[i]], true);
                terrains.Remove(keys[i]);
            }
        }
    }

    private void SpawnTerrains()
    {
        Vector2Int[] neighbours = GetNeighbours(_nowPos);
        foreach (Vector2Int nb in neighbours)
        {
            GameObject newTerrain = Terrain.CreateTerrainGameObject(datas[_dataID]);
            newTerrain.AddComponent(typeof(TerrainInit));
            newTerrain.GetComponent<TerrainInit>().InitTerrain(noiseMaterial, materials[_dataID], 
            resolution, nb, seed, koeff, fo, water,grass , grassTreshold, ruble);
            terrains.Add(nb, newTerrain);
            _dataID = (_dataID + 1) % 12;
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
            _nowPos = new Vector2Int((int)Mathf.Floor(ball.transform.position.x / resolution.y), (int)Mathf.Floor(ball.transform.position.z / resolution.y));
            if (_nowPos != _prePos)
            {
                SpawnTerrains();
                yield return null;
                DestroyOldTerrains();
            }
            yield return new WaitForFixedUpdate();
            if (!_isStarted && _dataID > 4)
            {
                _isStarted = true;
                StartGame();
            }
        }
    }

    public void SetGrassTreshold(float val)
    {
        grassTreshold = (1 - val) / 50 + 0.98f;
    }
}


