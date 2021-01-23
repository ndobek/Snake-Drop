using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSelector : MonoBehaviour, ISelectTerrain
{
    
    private GameObject selectedTerrain;
    public GameObject SelectedTerrain
    {
        get
        {
            return selectedTerrain;
        }
    }
    public void SelectTerrain(GameObject selection, ISpawnTerrain terrainSpawner)
    {
        terrainSpawner.DestroyTerrain();
        selectedTerrain = selection;
        terrainSpawner.SpawnTerrain();
    }

}
