using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "List of Saveable Plant Prefabs")]
public class SaveablePlantPrefabs : ScriptableObject
{
    [SerializeField]
    List<GameObject> PlantPrefabs = new List<GameObject>();

    public GameObject getPlantObject(string name)
    {
        foreach(GameObject obj in PlantPrefabs)
        {
            Plant plant = obj.GetComponent<Plant>();
            if (plant != null && plant.plantName == name) return obj;
        }
        return null;
    }

}
