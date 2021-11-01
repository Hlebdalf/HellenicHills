using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInit : MonoBehaviour
{
    private Texture2D _texture2;
    public Texture2D texture;
    private List<GameObject> _fOs = new List<GameObject>();
    private Material _noiseMaterial;
    private Material _spruceMaterial;
    private Material _terrainMaterial;
    private Vector2Int _resolution;
    private Vector2Int _offset;
    private float _seed;
    private int _koeff;
    private GameObject _refFo;
    private GameObject _water;
    private GameObject _grass;
    private GameObject _grassLPBatchRoot;
    private GameObject _grassHPBatchRoot;
    private List<GameObject> _grassesLP = new List<GameObject>();
    private List<GameObject> _grassesHP = new List<GameObject>();
    private void Awake()
    {
        GameObject[] grasses = GameObject.FindGameObjectsWithTag("Grass");
        if(grasses[0].name == "GrassHP")
        {
            _grassHPBatchRoot = grasses[0];
            _grassLPBatchRoot = grasses[1];
        }
        else
        {
            _grassHPBatchRoot = grasses[1];
            _grassLPBatchRoot = grasses[0];
        }
    }
    public void InitTerrain(Material ns, Material tr,
        Vector2Int res, Vector2Int oft, float sd, int kf, GameObject rFo, GameObject rWater, GameObject gr)
    {
        _noiseMaterial = ns; _terrainMaterial = tr;
        _resolution = res; _offset = oft; _seed = sd; _koeff = kf; _refFo = rFo;
        _water = Instantiate(rWater);
        _grass = gr;
        rWater.transform.position = new Vector3((_offset.x + 0.5f) * _resolution.x, 35, (_offset.y + 0.5f) * _resolution.x);
        StartCoroutine(Build());
    }
    void OnDestroy()
    {
        DestroyImmediate(_texture2, true);
        DestroyImmediate(texture, true);
        DestroyImmediate(_water, true);
        foreach (GameObject it in _fOs)
        {
            DestroyImmediate(it, true);
        }
        foreach (GameObject it in _grassesLP)
        {
            DestroyImmediate(it, true);
        }
        foreach (GameObject it in _grassesHP)
        {
            DestroyImmediate(it, true);
        }
        _fOs.Clear();
    }


    private IEnumerator Build()
    {
        _noiseMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", _offset.x + _seed);
        _noiseMaterial.SetFloat("Vector1_fd0d843ba4ac45c2bd344a013bfa0ab7", _offset.y);
        RenderTexture renderTexture = RenderTexture.GetTemporary(_resolution.y + 1, _resolution.y + 1);
        Graphics.Blit(null, renderTexture, _noiseMaterial);
        texture = new Texture2D(_resolution.y + 1, _resolution.y + 1);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(_resolution.y + 1, _resolution.y + 1)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        texture.Apply();
        if (Camera.main.GetComponent<GenScript>().terrains.ContainsKey(new Vector2Int(_offset.x - 1, _offset.y)))
        {
            texture.SetPixels(0, 0, 1, _resolution.x, Camera.main.GetComponent<GenScript>().terrains[new Vector2Int(_offset.x - 1, _offset.y)].GetComponent<TerrainInit>().texture.GetPixels(_resolution.x, 0, 1, _resolution.x));
        }
        if (Camera.main.GetComponent<GenScript>().terrains.ContainsKey(new Vector2Int(_offset.x, _offset.y + 1)))
        {
            texture.SetPixels(0, _resolution.x, _resolution.x, 1, Camera.main.GetComponent<GenScript>().terrains[new Vector2Int(_offset.x, _offset.y + 1)].GetComponent<TerrainInit>().texture.GetPixels(0, 0, _resolution.x, 1));
        }
        if (Camera.main.GetComponent<GenScript>().terrains.ContainsKey(new Vector2Int(_offset.x, _offset.y - 1)))
        {
            texture.SetPixels(0, 0, _resolution.x, 1, Camera.main.GetComponent<GenScript>().terrains[new Vector2Int(_offset.x, _offset.y - 1)].GetComponent<TerrainInit>().texture.GetPixels(0, _resolution.x, _resolution.x, 1));
        }
        gameObject.GetComponent<Terrain>().materialTemplate = _terrainMaterial;
        float[,] heightColors = new float[_resolution.y + 1, _resolution.y + 1];
        for (int p = 0; p < _resolution.y + 1; p++)
        {
            for (int y = 0; y < _resolution.y + 1; y++)
            {
                heightColors[p, y] = texture.GetPixel(y, p).a;
            }
        }
        gameObject.transform.position = new Vector3((_offset.x) * _resolution.y * _koeff, 0, (_offset.y) * _resolution.y * _koeff);
        gameObject.GetComponent<Terrain>().terrainData.heightmapResolution = _resolution.x + 1;
        gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, heightColors);
        gameObject.GetComponent<Terrain>().heightmapPixelError = 40;
        gameObject.GetComponent<Terrain>().terrainData.size = new Vector3((_resolution.x) * _koeff, 100, (_resolution.x) * _koeff);
        gameObject.GetComponent<Terrain>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        gameObject.GetComponent<Terrain>().materialTemplate.mainTexture = texture;
        for (int x = 0; x < _resolution.x; x++)
        {

            for (int y = 0; y < _resolution.y; y++)
            {
                float height = gameObject.GetComponent<Terrain>().terrainData.GetInterpolatedHeight(x / (float)_resolution.x, y / (float)_resolution.y);
                Vector3 normal = gameObject.GetComponent<Terrain>().terrainData.GetInterpolatedNormal(x / (float)_resolution.x, y / (float)_resolution.y);
                Color cl = texture.GetPixel(x, y);
                if (Random.value > 0.98f && height > 37 && !(texture.GetPixel(x, y).r > 0.65f && texture.GetPixel(x, y).g > 0.65f && texture.GetPixel(x, y).b > 0.65f))
                {   
                    GameObject gr = Instantiate(_grass);
                    gr.transform.position = transform.position + new Vector3(x * _koeff, height - 0.5f, y * _koeff);  
                    gr.transform.LookAt(normal + gr.transform.position - gr.transform.up);
                    _grassesHP.Add(gr.transform.GetChild(0).gameObject);
                    _grassesLP.Add(gr.transform.GetChild(1).gameObject);
                }
                if (Random.value > 0.998f)
                {
                    yield return null;
                    GameObject fo = Instantiate(_refFo);
                    
                    fo.transform.position = transform.position + new Vector3(x * _koeff, height, y * _koeff);
                    fo.GetComponent<FieldObject>().normal = normal;
                    _fOs.Add(fo);
                }
            }
        }
        /*StaticBatchingUtility.Combine(_grassesLP.ToArray(), _grassLPBatchRoot);
        StaticBatchingUtility.Combine(_grassesHP.ToArray(), _grassHPBatchRoot);*/
        /*_grassesLP.Clear(); BATCHING FEATURE
        _grassesHP.Clear();*/ 
        GetComponent<TerrainCollider>().enabled = true;
        yield return null;
    }
}
