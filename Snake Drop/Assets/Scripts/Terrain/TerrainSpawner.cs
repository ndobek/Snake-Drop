using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour, ISpawnTerrain
{
 
    public ISelectTerrain terrainSelector;
    private GameObject terrain;

    private void Start()
    {
        terrainSelector = GetComponentInParent<ISelectTerrain>();
    }
    public void SpawnTerrain()
    {
        terrain = Instantiate(terrainSelector.SelectedTerrain, transform);
    }
    public void DestroyTerrain()
    {
        if (terrain)
        {
            Destroy(terrain);
        }
    }
}
