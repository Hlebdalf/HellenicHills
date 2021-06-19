using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BakingScript : MonoBehaviour
{
    public Vector2Int Resolution = new Vector2Int(0, 0);
    public Material NoiseMaterial;
    public Material SpruceMaterial;
    public float seed;
    public GameObject Ball;
    public GameObject RefCharger;
    public UIButtonManager canvas;
    public GameObject RefSpruce;
    public Material TerrainMaterial;
    public GameObject TestBatch;
    public GameObject[] Terrains = new GameObject[2];
    public TerrainData[] Data = new TerrainData[2];
    public Texture2D[] HeightMaps = new Texture2D[2];
    private List<List<GameObject>> Spruces = new List<List<GameObject>>(2);
    private Transform BallTransform;
    public float border = 0;
    private bool isBallExist = false;
    private float spruceHardness = 0.7f;

    public float Shift;
    private void Awake()
    {
        BallTransform = Ball.GetComponent<Transform>();
        Ball.GetComponent<Transform>().position = new Vector3(-1, 0, Resolution.y);
        seed = Random.Range(-10000f, 10000f);
        for (int i = 0; i < 2; i++)
        {
            Spruces.Add(new List<GameObject>());
        }
        StartCoroutine(BuildTerrain());
    }
    void Start()
    {
        BallTransform.position = new Vector3(Resolution.y / 10, 75, Resolution.x / 2);
    }

    public Texture2D Bake(Vector2 offset)
    {
        NoiseMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", offset.x);
        NoiseMaterial.SetFloat("Vector1_fd0d843ba4ac45c2bd344a013bfa0ab7", offset.y + seed);
        NoiseMaterial.SetFloat("Vector1_090150e04f634e6eb9f7220d01725be0", seed);
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x + 1, Resolution.y + 1);
        Graphics.Blit(null, renderTexture, NoiseMaterial);
        Texture2D texture = new Texture2D(Resolution.x + 1, Resolution.y + 1);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.x + 1, Resolution.y + 1)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture;
    }
    public Texture2D BakeSpruce()
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x, Resolution.y);
        Graphics.Blit(null, renderTexture, SpruceMaterial);
        Texture2D texture = new Texture2D(Resolution.x, Resolution.y);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.x, Resolution.y)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture;
    }

    private void FixedUpdate()
    {
        if (BallTransform.position.x - border > 0)
        {
            border += Resolution.y;
            StartCoroutine(BuildTerrain());
        }
    }
    private void StartGame()
    {
        canvas.MenuUIActive();
    }
    IEnumerator BuildTerrain()
    {
        float X = Mathf.Floor(BallTransform.position.x / Resolution.y);
        float Z = Mathf.Floor(BallTransform.position.z / Resolution.y);
        print(Z);
        DestroyImmediate(Terrains[0], true);
        //DestroyImmediate(HeightMaps[0], true);
        Terrains[0] = Terrains[1];
        HeightMaps[0] = HeightMaps[1];
        Data[2] = Data[0];
        Data[0] = Data[1];
        Data[1] = Data[2];
        HeightMaps[1] = Bake(new Vector2(X, Z));
        foreach (GameObject it in Spruces[0])
        {
            DestroyImmediate(it, true);
        }
        yield return null;
        Spruces[0] = Spruces[1];
        Spruces[1] = new List<GameObject>();
        Texture2D SpruceMap = BakeSpruce();
        float[,] HeightColors = new float[Resolution.x + 1, Resolution.y + 1];
        for (int y = 0; y < Resolution.y + 1; y++)
        {
            if (y % 5 == 0)
            {
                yield return null;
            }
            for (int p = 0; p < Resolution.x + 1; p++)
            {
                HeightColors[p, y] = HeightMaps[1].GetPixel(y, p)[0];
                float SpruceHeight = HeightColors[p, y];
                if (SpruceMap.GetPixel(y, p).r > spruceHardness)
                {
                    if (Random.Range(-10.0f, 10.0f) > 9.2f)
                    {
                        GameObject Charger = Instantiate(RefCharger);
                        Charger.GetComponent<Transform>().position = new Vector3(X + y * Shift + 1000, SpruceHeight, Z + y * Shift);
                        Spruces[1].Add(Charger);
                        Charger.GetComponent<ChargerScript>().death = gameObject.GetComponent<Death>();
                    }
                    else
                    {
                        GameObject Spruce = Instantiate(RefSpruce);
                        Spruce.GetComponent<Transform>().position = new Vector3(X + y * Shift + 1000, SpruceHeight, Z + y * Shift);
                        Spruces[1].Add(Spruce);
                    }
                }
            }
        }
        StaticBatchingUtility.Combine(Spruces[1].ToArray(), TestBatch);
        GameObject NewTerrain = Terrain.CreateTerrainGameObject(Data[1]);
        NewTerrain.GetComponent<Terrain>().terrainData.heightmapResolution = Resolution.x + 1;
        NewTerrain.GetComponent<Terrain>().terrainData.size = new Vector3(Resolution.y, NewTerrain.GetComponent<Terrain>().terrainData.size.y, Resolution.x);
        NewTerrain.GetComponent<Terrain>().terrainData.SetHeights(0, 0, HeightColors);
        NewTerrain.GetComponent<Terrain>().materialTemplate = TerrainMaterial;
        NewTerrain.GetComponent<Terrain>().heightmapPixelError = 30;
        Transform NewTerrainTransform = NewTerrain.GetComponent<Transform>();
        NewTerrainTransform.position = new Vector3(X * Resolution.y + Resolution.y, 0, Z * Resolution.y - Resolution.y);
        Terrains[1] = NewTerrain;

        if (!isBallExist)
        {
            StartGame();
            isBallExist = true;
        }
        if (spruceHardness > 0.2f)
        {
            spruceHardness -= 0.1f;
        }
        yield break;
    }
}

