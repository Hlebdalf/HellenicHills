using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Vector2Int _resolution;
    private float height = 22;
    private float pixelError = 25;
    private List<GameObject> _fieldObjects = new List<GameObject>();
    private GameObject _terrain;
    private GameObject _water;
    private TerrainData _terrainData;
    private Texture2D _heightMap;
    private Texture2D _colorMap;
    private Material _noiseMaterial;
    private Material _terrainMaterial;
    private Vector2 _offset;  
    private List<GameObject> trash = new List<GameObject>();
    private void Awake()
    {
        Setup();
    }
    private void Start()
    {      
        _offset = new Vector2(transform.position.x / (_resolution.x  +1), transform.position.z /( _resolution.y+1)); 
        BakeHeights();
        BakeColor();
        StartCoroutine(BuildTerrain());
    }
    
    private void Setup()
    {
        _resolution = Settings.TerrainResolution;
        _water = Settings.Water;
        _terrainMaterial = Settings.TerrainMaterial;
        _noiseMaterial = Settings.NoiseMaterial;
        GameObject fos = Settings.FieldObjects;
        for (int i = 0; i < fos.transform.childCount; i++)
        {
            _fieldObjects.Add(fos.transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator BuildTerrain()
    {
        float[,] heights = new float[_resolution.y + 1, _resolution.y + 1];
        for (int p = 0; p < _resolution.y + 1; p++)
        {
            for (int y = 0; y < _resolution.y + 1; y++)
            {
                heights[p, y] = _heightMap.GetPixel(y, p).r;
            }
        }
        _terrainData = new TerrainData();
        _terrainData.heightmapResolution = _resolution.x;
        _terrainData.SetHeights(0,0,heights);
        _terrainData.size = new Vector3(_terrainData.size.x, height, _terrainData.size.z);
        _terrain = Terrain.CreateTerrainGameObject(_terrainData);
        _terrain.GetComponent<Terrain>().heightmapPixelError = pixelError;
        _terrain.transform.position = gameObject.transform.position;
        _terrain.transform.parent = gameObject.transform;
        Terrain tr = transform.GetChild(0).GetComponent<Terrain>();
        Material mt = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        GameObject wt = Instantiate(_water, new Vector3((_offset.x + 0.5f) * _resolution.x, 7, (_offset.y + 0.5f) * _resolution.y), Quaternion.identity);
        wt.transform.localScale = wt.transform.localScale * _resolution.y / 64;
        trash.Add(wt);
        mt.mainTexture = _colorMap;
        tr.materialTemplate = mt;
        
        for (int x = 0; x < _resolution.y + 1; x++)
        {
            for (int y = 0; y < _resolution.y + 1; y++)
            {
                float coin = Random.value;
                if (coin > 0.985f)
                {
                    yield return null;
                    int coin2 = (int)Mathf.Round(Random.value * 5);
                    float height = tr.terrainData.GetInterpolatedHeight(x / (float)_resolution.x, y / (float)_resolution.y);
                    Vector3 normal = tr.terrainData.GetInterpolatedNormal(x / (float)_resolution.x, y / (float)_resolution.y);
                    GameObject fieldObject;
                    if (coin2 < 3) normal = Vector3.up;
                    switch (coin2)
                    {
                        case 0:
                            fieldObject = Instantiate(_fieldObjects[0]);
                            break;
                        case 1:
                            fieldObject = Instantiate(_fieldObjects[1]);
                            break;
                        case 2:
                            fieldObject = Instantiate(_fieldObjects[2]);
                            break;
                        case 3:
                            fieldObject = Instantiate(_fieldObjects[3]);
                            break;
                        case 4:
                            fieldObject = Instantiate(_fieldObjects[4]);
                            break;
                        case 5:
                            fieldObject = Instantiate(_fieldObjects[5]);
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

        _noiseMaterial.SetFloat("atrey", _offset.x + 100);
        _noiseMaterial.SetFloat("persey", _offset.y + 100);
        _noiseMaterial.SetFloat("tesey", _resolution.y / 3);
        _heightMap = new Texture2D(_resolution.y + 1, _resolution.y + 1);      
        RenderTexture renderTexture = RenderTexture.GetTemporary(_resolution.y + 1, _resolution.y + 1);
        Graphics.Blit(null, renderTexture, _noiseMaterial);
        RenderTexture.active = renderTexture;
        _heightMap.ReadPixels(new Rect(Vector2.zero, new Vector2Int(_resolution.y + 1, _resolution.y + 1)), 0, 0);
        _heightMap.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
    }

    private void BakeColor()
    {
        int k = 4;
        
        _terrainMaterial.SetFloat("atrey", _offset.x+ 100);
        _terrainMaterial.SetFloat("persey", _offset.y + 100);
        _terrainMaterial.SetFloat("tesey", _resolution.y/3);
        _colorMap = new Texture2D(k * _resolution.y + 1, k * _resolution.y + 1);
        RenderTexture renderTexture = RenderTexture.GetTemporary(k * _resolution.y + 1, k * _resolution.y + 1);
        Graphics.Blit(null, renderTexture, _terrainMaterial);
        RenderTexture.active = renderTexture;
        _colorMap.ReadPixels(new Rect(Vector2.zero, new Vector2Int(k * _resolution.y + 1, k * _resolution.y + 1)), 0, 0);
        _colorMap.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
    }
}
