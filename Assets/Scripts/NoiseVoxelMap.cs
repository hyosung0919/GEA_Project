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

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                    Place(x, y, z, h);
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
    private void Place(int x, int y, int z, int h)
    {

        if (y == h)
        {
            var go = Instantiate(grassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
            go.name = $"B_{x}_{y},{z}";
        }
        else
        {
            var go = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
            go.name = $"B_{x}_{y},{z}";
        }
    }
    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(waterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"W_{x}_{y}_{z}";
    }
}
