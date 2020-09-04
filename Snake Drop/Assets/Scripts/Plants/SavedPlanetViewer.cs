using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedPlanetViewer : MonoBehaviour
{

    public Text score;
    public SaveablePlantPrefabs prefabs;
    public List<Plant> plants = new List<Plant>();
    public void LoadPlanet(SaveData saveData)
    {
        DestroyPlants();
        foreach(PlantSaveData obj in saveData.planetData.plantData)
        {
            GameObject spawnedGameObject = GameObject.Instantiate(prefabs.getPlantObject(obj.plantName), gameObject.transform);
            Plant spawnedPlant = spawnedGameObject.GetComponent<Plant>();

            spawnedPlant.LoadPlant(obj);
            plants.Add(spawnedPlant);

        }
        score.text = saveData.score.ToString();
    }

    public void LoadHighScore()
    {
        LoadPlanet(SaveManager.LoadHighScore());
    }

    public void UpdatePlants()
    {
        foreach(Plant obj in plants)
        {
            obj.UpdatePlant();
        }
    }

    private void DestroyPlants()
    {
        for(int i = 0; i < plants.Count; i++)
        {
            if(plants[i] != null) GameObject.Destroy(plants[i].gameObject);
        }
    }

    private void Update()
    {
        UpdatePlants();
    }
}
