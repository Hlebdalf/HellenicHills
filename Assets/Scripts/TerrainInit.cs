using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInit : MonoBehaviour
{
    public void InitTerrain(Material noiseMaterial, Material spruceMaterial, Material refMaterial, Vector2Int Resolution, Vector2Int offset, float seed, int koeff)
    {
        noiseMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", offset.x + seed);
        noiseMaterial.SetFloat("Vector1_fd0d843ba4ac45c2bd344a013bfa0ab7", offset.y);
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.y + 1, Resolution.y + 1);
        Graphics.Blit(null, renderTexture, noiseMaterial);
        Texture2D texture = new Texture2D(Resolution.y + 1, Resolution.y + 1);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, new Vector2Int(Resolution.y + 1, Resolution.y + 1)), 0, 0);
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        texture.Apply();
        gameObject.GetComponent<Terrain>().materialTemplate = refMaterial;
        float[,] HeightColors = new float[Resolution.y + 1, Resolution.y + 1];
        for (int p = 0; p < Resolution.y + 1; p++)
        {
            for (int y = 0; y < Resolution.y + 1; y++)
            {
                HeightColors[p, y] = texture.GetPixel(y, p).a;
            }
        }

        gameObject.transform.position = new Vector3((offset.x) * Resolution.y * koeff, 0, (offset.y) * Resolution.y * koeff);
        gameObject.GetComponent<Terrain>().terrainData.heightmapResolution = Resolution.x+1;
        gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, HeightColors);
        gameObject.GetComponent<Terrain>().heightmapPixelError = 30;
        gameObject.GetComponent<Terrain>().terrainData.size = new Vector3(Resolution.x * koeff, 100, Resolution.x * koeff);
        gameObject.GetComponent<Terrain>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }


}
