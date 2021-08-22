using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInit : MonoBehaviour
{
    private Texture2D texture2;
    public Texture2D texture;
    private List<GameObject> FOs = new List<GameObject>();
    private Material noiseMaterial;
    private Material spruceMaterial;
    private Material terrainMaterial;
    private Vector2Int Resolution;
    private Vector2Int offset;
    private float seed;
    private int koeff;
    private GameObject refFO;
    public void InitTerrain(Material ns, Material sp, Material tr,
        Vector2Int res, Vector2Int oft, float sd, int kf, GameObject rFO)
    {
        noiseMaterial = ns; spruceMaterial = sp; terrainMaterial = tr;
        Resolution = res; offset = oft; seed = sd; koeff = kf; refFO = rFO;
        StartCoroutine(Build());
    }
    void OnDestroy()
    {
        DestroyImmediate(texture2, true);
        DestroyImmediate(texture, true);
        foreach (GameObject it in FOs)
        {
            DestroyImmediate(it, true);
        }
        FOs.Clear();
    }


    private IEnumerator Build()
    {
        noiseMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", offset.x + seed);
        noiseMaterial.SetFloat("Vector1_fd0d843ba4ac45c2bd344a013bfa0ab7", offset.y);
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.y + 1, Resolution.y + 1);
        Graphics.Blit(null, renderTexture, noiseMaterial);
        texture = new Texture2D(Resolution.y + 1, Resolution.y + 1);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.y + 1, Resolution.y + 1)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        texture.Apply();
        if (Camera.main.GetComponent<GenScript>().terrains.ContainsKey(new Vector2Int(offset.x - 1, offset.y)))
        {
            texture.SetPixels(0, 0, 1, Resolution.x, Camera.main.GetComponent<GenScript>().terrains[new Vector2Int(offset.x - 1, offset.y)].GetComponent<TerrainInit>().texture.GetPixels(Resolution.x, 0, 1, Resolution.x));
        }
        if (Camera.main.GetComponent<GenScript>().terrains.ContainsKey(new Vector2Int(offset.x, offset.y + 1)))
        {
            texture.SetPixels(0, Resolution.x, Resolution.x, 1, Camera.main.GetComponent<GenScript>().terrains[new Vector2Int(offset.x, offset.y + 1)].GetComponent<TerrainInit>().texture.GetPixels(0, 0, Resolution.x, 1));
        }
        if (Camera.main.GetComponent<GenScript>().terrains.ContainsKey(new Vector2Int(offset.x, offset.y - 1)))
        {
            texture.SetPixels(0, 0, Resolution.x, 1, Camera.main.GetComponent<GenScript>().terrains[new Vector2Int(offset.x, offset.y - 1)].GetComponent<TerrainInit>().texture.GetPixels(0, Resolution.x, Resolution.x, 1));
        }
        gameObject.GetComponent<Terrain>().materialTemplate = terrainMaterial;
        float[,] HeightColors = new float[Resolution.y + 1, Resolution.y + 1];
        for (int p = 0; p < Resolution.y + 1; p++)
        {
            for (int y = 0; y < Resolution.y + 1; y++)
            {
                HeightColors[p, y] = texture.GetPixel(y, p).a;
            }
        }
        gameObject.transform.position = new Vector3((offset.x) * Resolution.y * koeff, 0, (offset.y) * Resolution.y * koeff);
        gameObject.GetComponent<Terrain>().terrainData.heightmapResolution = Resolution.x + 1;
        gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, HeightColors);
        gameObject.GetComponent<Terrain>().heightmapPixelError = 40;
        gameObject.GetComponent<Terrain>().terrainData.size = new Vector3((Resolution.x) * koeff, 100, (Resolution.x) * koeff);
        gameObject.GetComponent<Terrain>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        gameObject.GetComponent<Terrain>().materialTemplate.mainTexture = texture;


        texture2 = new Texture2D(Resolution.y + 1, Resolution.y + 1);
        Graphics.Blit(null, renderTexture, spruceMaterial);
        RenderTexture.active = renderTexture;
        texture2.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.x * koeff, Resolution.y * koeff)), 0, 0);
        texture2.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        for (int x = 0; x < Resolution.x; x++)
        {

            for (int y = 0; y < Resolution.y; y++)
            {
                if (texture2.GetPixel(x, y).r > 0.5f)
                {
                    yield return null;
                    GameObject FO = Instantiate(refFO);
                    float height = gameObject.GetComponent<Terrain>().terrainData.GetInterpolatedHeight(x / (float)Resolution.x, y / (float)Resolution.y);
                    FO.transform.position = transform.position + new Vector3(x * koeff, height, y * koeff);
                    FO.GetComponent<FieldObject>().normal = gameObject.GetComponent<Terrain>().terrainData.GetInterpolatedNormal(x / (float)Resolution.x, y / (float)Resolution.y);
                    FOs.Add(FO);
                }
            }
        }

        
        yield return null;
    }
}
