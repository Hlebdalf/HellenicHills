using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Vector2Int resolution = new Vector2Int(64, 64);
    private float height = 30;
    private float pixelError = 25;
    private GameObject _terrain;
    private TerrainData _terrainData;
    private Texture2D _heightMap;
    private Material _noiseMaterial;
    private Vector2 _offset;
    private void Start()
    {
        Debug.Log(transform.position);
        _offset = new Vector2(transform.position.x / (resolution.x  +1), transform.position.z /( resolution.y+1));
        BakeHeights();
        BuildTerrain();
    }

    private void BuildTerrain()
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
    }

    private void BakeHeights()
    {
        _noiseMaterial = Resources.Load("NoiseMaterial") as Material;
        _noiseMaterial.SetFloat("atrey", _offset.x + 100);
        _noiseMaterial.SetFloat("persey", _offset.y + 100);
        _noiseMaterial = Resources.Load("NoiseMaterial") as Material;
        _heightMap = new Texture2D(resolution.y + 1, resolution.y + 1);      
        RenderTexture renderTexture = RenderTexture.GetTemporary(resolution.y + 1, resolution.y + 1);
        Graphics.Blit(null, renderTexture, _noiseMaterial);
        RenderTexture.active = renderTexture;
        _heightMap.ReadPixels(new Rect(Vector2.zero, new Vector2Int(resolution.y + 1, resolution.y + 1)), 0, 0);
        _heightMap.Apply();
    }
}
