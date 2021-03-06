using UnityEditor;
using UnityEngine;
using System.IO;


public class BakeTextureWindow : EditorWindow
{
    [MenuItem("Tools/Bake material to texture")]
    static void OpenWindow()
    {
        BakeTextureWindow window = EditorWindow.GetWindow<BakeTextureWindow>();
        window.Show();
    }
 
    Material ImageMaterial;
    string FilePath = "Assets/MaterialImage.png";
    Vector2Int Resolution;

    void OnGUI()
    {
        ImageMaterial = (Material)EditorGUILayout.ObjectField("Material", ImageMaterial, typeof(Material), false);
        Resolution = EditorGUILayout.Vector2IntField("Image Resolution", Resolution);
        FilePath = EditorGUILayout.TextField("Image Path", FilePath);

        if (GUILayout.Button("Bake"))
        {
            BakeTexture();
        }
    }
    void BakeTexture()
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(Resolution.x, Resolution.y);
        Graphics.Blit(null, renderTexture, ImageMaterial);
        Texture2D texture = new Texture2D(Resolution.x, Resolution.y);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(Vector2.zero, Resolution), 0, 0);
        byte[] png = texture.EncodeToPNG();
        File.WriteAllBytes(FilePath, png);
        AssetDatabase.Refresh();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
        DestroyImmediate(texture);
    }
}    