using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject grassPrefab;
    public GameObject waterPrefab;
    public int width = 20;
    public int depth = 20;
    public int maxHeight = 16;      // Y
    public int waterHeight = 6;
    [SerializeField] float noiseScale = 20f;
    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) h = 1;

                for (int y = 0; y <= h; y++)
                {
                    if (y == h)
                        PlaceGrass(x, y, z);
                    else
                        Place(x, y, z);
                }
                if (h < waterHeight)
                {
                    for (int y = h + 1; y <= waterHeight; y++)
                    {
                        PlaceWater(x, y, z);
                    }
                }

            }

        }
    }
    private void Place(int x, int y, int z)
    {
        {
            var go = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
            go.name = $"B_{x}_{y},{z}";

            var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
            b.type = BlockType.Dirt;
            b.maxHP = 3;
            b.dropCount = 1;
            b.mineable = true;
        }
    }
    private void PlaceGrass(int x, int y , int z)
    {
        var go = Instantiate(grassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_{x}_{y},{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Grass;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }
    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(waterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"W_{x}_{y}_{z}";
    }

    public void PlaceTile(Vector3Int pos, BlockType type)
    {
        switch (type)
        {
            case BlockType.Dirt:
                Place(pos.x, pos.y, pos.z);
                break;
            case BlockType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;            
            case BlockType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;
        }
    }
}
