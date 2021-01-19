using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleAvailableTerrains : MonoBehaviour
{
    [SerializeField]
    public ISelectTerrain terrainSelector;
    public List<GameObject> terrains;
    private int i = 0;
    private GameObject selection;
    public ISpawnTerrain terrainSpawner;

    private void Start()
    {
        terrainSelector = GetComponentInParent<ISelectTerrain>();
        terrainSpawner = GetComponentInChildren<ISpawnTerrain>();
        
    }
    public void OnButtonClicked()
    {
        if (i + 1 > terrains.Count){i = 0;}
        selection = terrains[i];
        i++;
        
        terrainSelector.SelectTerrain(selection, terrainSpawner);
    }
}
