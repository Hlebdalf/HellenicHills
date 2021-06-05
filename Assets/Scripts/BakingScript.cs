//using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;


public class BakingScript : MonoBehaviour
{
    public Vector2Int Resolution = new Vector2Int(0, 0);
    public Material ImageMaterial;
    public string FilePath = "Assets/Textures/MaterialImage";
    public float seed;
    public GameObject Ball;
    public GameObject RefTerrain;
    private Transform BallTransform;
    private float border = -1000;
    public GameObject[] Terrains = new GameObject[6];
    public TerrainData[] Data = new TerrainData[6];
    public Texture2D[] HeightMaps = new Texture2D[6];
    private TerrainData[] SwitchData = new TerrainData[3];

    private void Awake()
    {
        BallTransform = Ball.GetComponent<Transform>();
        seed = Random.Range(-10000f, 10000f);
        BuildTerrain();
        
    }
    void Start()
    {
        BallTransform.position = new Vector3(10, 100, 500);
    }

    public Texture2D Bake(Vector2 offset)
    {
        ImageMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", seed + offset.x);
        ImageMaterial.SetFloat("Vector1_fd0d843ba4ac45c2bd344a013bfa0ab7", offset.y);
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x, Resolution.y);
        Graphics.Blit(null, renderTexture, ImageMaterial);
        Texture2D texture = new Texture2D(Resolution.x, Resolution.y);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, Resolution), 0, 0);
        AssetDatabase.Refresh();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture;
    }

    private void FixedUpdate()
    {
        if (BallTransform.position.x - border > 1010)
        {
            border += 1000;
            BuildTerrain();
        }
    }
    private void BuildTerrain()
    {
        float X = Mathf.Floor(BallTransform.position.x / 1000) * 1000;
        float Z = Mathf.Floor(BallTransform.position.z / 1000) * 1000;
        
        for (int i = -1; i < 2; i++)
        {
            DestroyImmediate(Terrains[i + 1], true);
            DestroyImmediate(HeightMaps[i + 1], true);
            Terrains[i + 1] = Terrains[i + 4];
            HeightMaps[i + 1] = HeightMaps[i + 4];
            SwitchData[i+1] = Data[i + 1];
            Data[i + 1] = Data[i + 4];
            Data[i + 4] = SwitchData[i + 1];
            HeightMaps[i + 4] = Bake(new Vector2(X, Z * i));
            float[,] HeightColors = new float[Resolution.x, Resolution.y];
            for (int y = 0; y < Resolution.x; y++)
            {
                for (int p = 0; p < Resolution.y; p++)
                {
                    HeightColors[y, p] = HeightMaps[i+4].GetPixel(y,p)[0]/10;
                }
            }
            GameObject NewTerrain = Terrain.CreateTerrainGameObject(Data[i+4]);
            NewTerrain.GetComponent<Terrain>().terrainData.heightmapResolution = Resolution.x  + 1;
            NewTerrain.GetComponent<Terrain>().terrainData.SetHeights(0, 0, HeightColors);
            //NewTerrain.GetComponent<Terrain>().terrainData.heightmapScale.Scale(new Vector3(0.5f, 0.5f, 0.5f));
            //NewTerrain.GetComponent<Terrain>().terrainData.
            Transform NewTerrainTransform = NewTerrain.GetComponent<Transform>();
            NewTerrainTransform.position = new Vector3(X + 1000, 0, Z + 1000 * i);           
            Terrains[i + 4] = NewTerrain;             
            Debug.Log("Good Luck!");
        }
    }
 
}

