using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlanetSpawner : MonoBehaviour
{
    public SaveablePlantPrefabs prefabs;
    public void LoadPlanet(PlanetSaveData saveData)
    {
        foreach(PlantSaveData obj in saveData.plantData)
        {
            GameObject spawnedGameObject = GameObject.Instantiate(prefabs.getPlantObject(obj.plantName), gameObject.transform);
            Plant spawnedPlant = spawnedGameObject.GetComponent<Plant>();

            spawnedPlant.LoadPlant(obj);

        }
    }

    public void LoadHighScore()
    {
        LoadPlanet(SaveManager.LoadByName("High Score") as PlanetSaveData);
    }
}
