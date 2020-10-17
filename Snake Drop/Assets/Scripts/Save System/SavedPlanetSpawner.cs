using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlanetSpawner : MonoBehaviour
{
    public GameObject planetViewer;
    public PlanetManager manager;
    public bool excludeHighScore;

    private void Awake()
    {
        LoadAll();
    }

    public void LoadAll()
    {
        string[] saves = SaveManager.GetAllSaveNames();
        int quantity = saves.Length;
        for (int i = 0; i < quantity; i++)
        {
            if (!(excludeHighScore && saves[i] == SaveManager.HighScoreSaveName))
            {
                GameObject spawned = Instantiate(planetViewer, transform);
                spawned.transform.position = new Vector3(0, spawned.transform.position.y);
                SavedPlanetViewer planet = spawned.GetComponent<SavedPlanetViewer>();
                planet.LoadPlanet(saves[i]);
                planet.SetManager(manager);
            }
        }
    }

}
