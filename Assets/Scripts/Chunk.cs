using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public static  Vector2Int resolution = new Vector2Int(64, 64);
    private float height = 22;
    private float pixelError = 25;
    private GameObject _terrain;
    private TerrainData _terrainData;
    private Texture2D _heightMap;
    private Texture2D _colorMap;
    private Material _noiseMaterial;
    private Material _colorMaterial;
    private Vector2 _offset;
    public List<GameObject> FieldObjects = new List<GameObject>();
    private List<GameObject> trash = new List<GameObject>();
    private void Start()
    {
        _offset = new Vector2(transform.position.x / (resolution.x  +1), transform.position.z /( resolution.y+1));
        LoadResources();
        BakeHeights();
        BakeColor();
        StartCoroutine(BuildTerrain());
    }
    private void LoadResources()
    {
        FieldObjects.Add(Resources.Load("Birch") as GameObject);
        FieldObjects.Add(Resources.Load("Pine") as GameObject);
        FieldObjects.Add(Resources.Load("Sakura") as GameObject);
        FieldObjects.Add(Resources.Load("Stone1") as GameObject);
        FieldObjects.Add(Resources.Load("Stone2") as GameObject);
        FieldObjects.Add(Resources.Load("Stone3") as GameObject);
    }

    private IEnumerator BuildTerrain()
    {
        float[,] heights = new float[resolution.y + 1, resolution.y + 1];
        for (int p = 0; p < resolution.y + 1; p++)
        {
            for (int y = 0; y < resolution.y + 1; y++)
            {
                heights[p, y] = _heightMap.GetPixel(y, p).r;
            }
        }
        _terrainData = new TerrainData();
        _terrainData.heightmapResolution = resolution.x;
        _terrainData.SetHeights(0,0,heights);
        _terrainData.size = new Vector3(_terrainData.size.x, height, _terrainData.size.z);
        _terrain = Terrain.CreateTerrainGameObject(_terrainData);
        _terrain.GetComponent<Terrain>().heightmapPixelError = pixelError;
        _terrain.transform.position = gameObject.transform.position;
        _terrain.transform.parent = gameObject.transform;
        Terrain tr = transform.GetChild(0).GetComponent<Terrain>();
        Material mt = new Material(Shader.Find("Unlit/Texture"));
        mt.mainTexture = _colorMap;
        tr.materialTemplate = mt;
        
        for (int x = 0; x < resolution.y + 1; x++)
        {
            for (int y = 0; y < resolution.y + 1; y++)
            {
                float coin = Random.value;
                if (coin > 0.975f)
                {
                    yield return null;
                    int coin2 = (int)Mathf.Round(Random.value * 5);
                    float height = tr.terrainData.GetInterpolatedHeight(x / (float)resolution.x, y / (float)resolution.y);
                    Vector3 normal = tr.terrainData.GetInterpolatedNormal(x / (float)resolution.x, y / (float)resolution.y);
                    GameObject fieldObject;
                    if (coin2 < 3) normal = Vector3.up;
                    switch (coin2)
                    {
                        case 0:
                            fieldObject = Instantiate(FieldObjects[0]);
                            break;
                        case 1:
                            fieldObject = Instantiate(FieldObjects[1]);
                            break;
                        case 2:
                            fieldObject = Instantiate(FieldObjects[2]);
                            break;
                        case 3:
                            fieldObject = Instantiate(FieldObjects[3]);
                            break;
                        case 4:
                            fieldObject = Instantiate(FieldObjects[4]);
                            break;
                        case 5:
                            fieldObject = Instantiate(FieldObjects[5]);
                            break;
                        default:
                            fieldObject = new GameObject(); Debug.Log(coin2);
                            break;
                    }
                    fieldObject.transform.position = transform.position + new Vector3(x, height - 0.5f, y);
                    fieldObject.transform.LookAt(normal + fieldObject.transform.position - fieldObject.transform.up);
                    trash.Add(fieldObject);
                }

            }
        }

    }
    private void OnDestroy()
    {
        foreach(GameObject it in trash)
        {
            Destroy(it);
        }
    }

    private void BakeHeights()
    {
        _noiseMaterial = Resources.Load("NoiseMaterial") as Material;
        _noiseMaterial.SetFloat("atrey", _offset.x + 100);
        _noiseMaterial.SetFloat("persey", _offset.y + 100);
        _heightMap = new Texture2D(resolution.y + 1, resolution.y + 1);      
        RenderTexture renderTexture = RenderTexture.GetTemporary(resolution.y + 1, resolution.y + 1);
        Graphics.Blit(null, renderTexture, _noiseMaterial);
        RenderTexture.active = renderTexture;
        _heightMap.ReadPixels(new Rect(Vector2.zero, new Vector2Int(resolution.y + 1, resolution.y + 1)), 0, 0);
        _heightMap.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
    }

    private void BakeColor()
    {
        int k = 4;
        _colorMaterial = Resources.Load("TerrainMaterial") as Material;
        _colorMaterial.SetFloat("atrey", _offset.x+ 100);
        _colorMaterial.SetFloat("persey", _offset.y + 100);
        _colorMap = new Texture2D(k * resolution.y + 1, k * resolution.y + 1);
        RenderTexture renderTexture = RenderTexture.GetTemporary(k * resolution.y + 1, k * resolution.y + 1);
        Graphics.Blit(null, renderTexture, _colorMaterial);
        RenderTexture.active = renderTexture;
        _colorMap.ReadPixels(new Rect(Vector2.zero, new Vector2Int(k * resolution.y + 1, k * resolution.y + 1)), 0, 0);
        _colorMap.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
    }
}
