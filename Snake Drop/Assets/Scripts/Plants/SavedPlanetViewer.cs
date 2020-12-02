using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedPlanetViewer : MonoBehaviour
{
    public string saveName;
    public Text score;
    public SaveableObjects prefabs;
    public List<Plant> plants = new List<Plant>();

    public void Awake()
    {
        if (saveName != null) LoadPlanet(saveName);
    }
    public void LoadPlanet(string name)
    {
        saveName = name;
        LoadPlanet(SaveManager.LoadGame(name));
    }
    public void DeleteSaveFile()
    {
        SaveManager.DeleteSave(saveName);
        Destroy(this.gameObject);
    }
    public void LoadPlanet(SaveData saveData)
    {
        if (saveData != null)
        {
            DestroyPlants();
            foreach (PlantSaveData obj in saveData.planetData.plantData)
            {
                GameObject spawnedGameObject = GameObject.Instantiate(prefabs.getObject(obj.plantName) as GameObject, gameObject.transform);
                Plant spawnedPlant = spawnedGameObject.GetComponent<Plant>();

                spawnedPlant.LoadPlant(obj);
                plants.Add(spawnedPlant);

            }
            score.text = saveData.score.ToString();
        }
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
