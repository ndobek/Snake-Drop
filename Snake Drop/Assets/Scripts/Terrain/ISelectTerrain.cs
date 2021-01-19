using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectTerrain
{
    void SelectTerrain(GameObject selection, ISpawnTerrain terrainSpawner);
    GameObject SelectedTerrain 
    {
        get;
    }
}
