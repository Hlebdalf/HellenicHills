using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings 
{
    public static Material NoiseMaterial = Resources.Load("NoiseMaterial") as Material;
    public static Material TerrainMaterial = Resources.Load("TerrainMaterial") as Material;
    public static Vector2Int TerrainResolution = new Vector2Int(32, 32);
    public static GameObject FieldObjects = Resources.Load("FieldObjects") as GameObject;
    public static GameObject Water = Resources.Load("WaterPlane") as GameObject;
}
