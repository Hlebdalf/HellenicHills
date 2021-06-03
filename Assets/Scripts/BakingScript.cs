using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;


public class BakingScript : MonoBehaviour
{
    public int cnt;
    public Vector2Int Resolution = new Vector2Int(0, 0);
    public Material ImageMaterial;
    public string FilePath = "Assets/Textures/MaterialImage";
    public int seed = 0;
    void Start()
    {
        Bake(cnt);
    }

    public void Bake(int num)
    {
        for (int i = 0; i < num; i++)
        {
            ImageMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", seed);
            seed += 10;
            RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x, Resolution.y);
            Graphics.Blit(null, renderTexture, ImageMaterial);
            Texture2D texture = new Texture2D(Resolution.x, Resolution.y);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(Vector2.zero, Resolution), 0, 0);
            byte[] png = texture.EncodeToPNG();
            File.WriteAllBytes(FilePath + (seed/10).ToString() + ".png", png);
            AssetDatabase.Refresh();
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);
            DestroyImmediate(texture);
            
        }
    }
}
