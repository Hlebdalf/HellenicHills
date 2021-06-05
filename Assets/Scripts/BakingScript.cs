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
    public GameObject Ball;
    public GameObject RefTerrain;
    private Transform BallTransform;
    private float border = -1000;
    //private GameObject[] Trash = new GameObject[6];
    void Start()
    {
        //Bake(cnt);
        BallTransform = Ball.GetComponent<Transform>();
    }

    public void Bake(Vector2 offset)
    {
        /*ImageMaterial.SetFloat("Vector1_2890a1d24f7f415986e2ea5c2f0e3b46", seed);
        seed += 10;
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x, Resolution.y);
        Graphics.Blit(null, renderTexture, ImageMaterial);
        Texture2D texture = new Texture2D(Resolution.x, Resolution.y);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, Resolution), 0, 0);
        byte[] png = texture.EncodeToPNG();
        File.WriteAllBytes(FilePath + (seed / 10).ToString() + ".png", png);
        //AssetDatabase.Refresh();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        DestroyImmediate(texture);*/
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
            GameObject NewTerrain = Instantiate(RefTerrain);
            Transform NewTerrainTransform = NewTerrain.GetComponent<Transform>();
            NewTerrainTransform.position = new Vector3(X + 1000, 0, Z + 1000 * i);
        }
    }
}
