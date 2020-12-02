using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlanetSpawner : MonoBehaviour
{
    public GameObject[] preExistingPlanetViewers;
    public GameObject planetViewerPrefab;
    public SwipeMenuManager SwipeMenuManager;

    private void Awake()
    {
        LoadAll();
    }

    //public void LoadAll()
    //{
    //    string[] saves = SaveManager.GetAllSaveNames();
    //    foreach (string save in saves)
    //    {
    //        GameObject spawned = Instantiate(planetViewer, SwipeMenuManager.transform);
    //        spawned.GetComponent<SavedPlanetViewer>().LoadPlanet(save);
    //        SwipeMenuManager.Add(spawned.transform);
    //    }
    //}

    public void LoadAll()
    {
        string[] saves = SaveManager.GetAllSaveNames();
        for(int i = 0; i < saves.Length; i++)
        {
            GameObject planetSpawner;
            if (i < preExistingPlanetViewers.Length && preExistingPlanetViewers[i] != null) planetSpawner = preExistingPlanetViewers[i];
            else
            {
                planetSpawner = Instantiate(planetViewerPrefab, SwipeMenuManager.transform);
                SwipeMenuManager.Add(planetSpawner.transform);
            }

            planetSpawner.GetComponent<SavedPlanetViewer>().LoadPlanet(saves[i]);
        }
    }

}
