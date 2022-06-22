using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public Dictionary<Vector2Int, GameObject> terrains = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int _nowPos;
    private Vector2Int _prePos;

    private void Start()
    {
        _prePos = new Vector2Int(-Chunk.resolution.x, 0);
    }
    private void FixedUpdate()
    {      
        _nowPos = new Vector2Int((int)Mathf.Floor(transform.position.x / Chunk.resolution.x), (int)Mathf.Floor(transform.position.z / Chunk.resolution.y));
        if (_nowPos != _prePos)
        {
            _prePos = _nowPos;
            SpawnTerrains();
            DestroyOldTerrains();
        }
    }

    private Vector2Int[] GetNeighbours(Vector2Int pos)
    {
        Vector2Int[] allNbhs = new Vector2Int[6];
        List<Vector2Int> result = new List<Vector2Int>();
        allNbhs[0] = new Vector2Int(pos.x, pos.y);
        allNbhs[1] = new Vector2Int(pos.x, pos.y - 1);
        allNbhs[2] = new Vector2Int(pos.x, pos.y + 1);
        allNbhs[3] = new Vector2Int(pos.x + 1, pos.y);
        allNbhs[4] = new Vector2Int(pos.x + 1, pos.y - 1);
        allNbhs[5] = new Vector2Int(pos.x + 1, pos.y + 1);

        for (int i = 0; i < 6; i++)
        {
            if (!terrains.ContainsKey(allNbhs[i])) result.Add(allNbhs[i]);
        }
        return result.ToArray();
    }

    private void SpawnTerrains()
    {
        Vector2Int[] neighbours = GetNeighbours(_nowPos);
        foreach (Vector2Int nb in neighbours)
        {
            GameObject terrainObject = Instantiate(new GameObject(), new Vector3(nb.x * Chunk.resolution.x, 0, nb.y * Chunk.resolution.y), Quaternion.identity);
            terrainObject.AddComponent<Chunk>();
            terrains.Add(nb, terrainObject);
        }

    }

    private void DestroyOldTerrains()
    {
        List<Vector2Int> keys = terrains.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            if (terrains[keys[i]].transform.position.x < transform.position.x - Chunk.resolution.y - 10)
            {          
                DestroyImmediate(terrains[keys[i]].transform.parent, true);
                DestroyImmediate(terrains[keys[i]], true);
                terrains.Remove(keys[i]);
            }
        }
    }
}
