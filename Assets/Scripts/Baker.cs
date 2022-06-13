using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Baker: MonoBehaviour
{
    public Material _noiseMaterial;
    public Vector2Int resolution = new Vector2Int(164, 164);
    private Texture2D heightMap;
    public Texture2D GetTexture()
    {
        return heightMap;
    }

    /*private void Start()
    {
        heightMap = new Texture2D(resolution.y + 1, resolution.y + 1);
        _noiseMaterial = Resources.Load("NoiseMaterial") as Material;
        RenderTexture renderTexture = RenderTexture.GetTemporary(resolution.y + 1, resolution.y + 1);
        Graphics.Blit(null, renderTexture, _noiseMaterial);      
        RenderTexture.active = renderTexture;
        heightMap.ReadPixels(new Rect(Vector2.zero, new Vector2Int(resolution.y + 1, resolution.y + 1)), 0, 0);     
        heightMap.Apply();
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = heightMap;
    }*/
}
