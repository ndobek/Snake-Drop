using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour, ISpawnTerrain
{
 
    public ISelectTerrain terrainSelector;
    public GameObject terrain;

    private void Start()
    {
        terrainSelector = GetComponentInParent<ISelectTerrain>();
        SpawnTerrain();
    }
    public void SpawnTerrain()
    {
        if (terrainSelector.SelectedTerrain)
        {

            terrain = Instantiate(terrainSelector.SelectedTerrain, transform);
        }
        else
        {
            terrain = Instantiate(terrain, transform);
        }
    }
    public void DestroyTerrain()
    {
        if (terrain)
        {
            Destroy(terrain);
        }
    }
}
