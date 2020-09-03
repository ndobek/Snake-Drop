using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlanetSpawner : MonoBehaviour
{
    public SaveablePlantPrefabs prefabs;
    public List<Plant> plants = new List<Plant>();
    public void LoadPlanet(PlanetSaveData saveData)
    {
        foreach(PlantSaveData obj in saveData.plantData)
        {
            GameObject spawnedGameObject = GameObject.Instantiate(prefabs.getPlantObject(obj.plantName), gameObject.transform);
            Plant spawnedPlant = spawnedGameObject.GetComponent<Plant>();

            spawnedPlant.LoadPlant(obj);
            plants.Add(spawnedPlant);

        }
    }

    public void LoadHighScore()
    {
        LoadPlanet(SaveManager.LoadByName("High Score") as PlanetSaveData);
    }

    public void UpdatePlants()
    {
        foreach(Plant obj in plants)
        {
            obj.UpdatePlant();
        }
    }

    private void Update()
    {
        UpdatePlants();
    }
}
