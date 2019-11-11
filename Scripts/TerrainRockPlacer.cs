using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRockPlacer : MonoBehaviour
{
    public Terrain targetTerrain;
    public GameObject[] rockPrefabs;
    public int numberOfRocks;
    private TerrainData td;

    private void Start()
    {
        PlaceRandomnRocks();
    }

    public void PlaceRandomnRocks()
    {
        td = targetTerrain.terrainData;
    }

}
