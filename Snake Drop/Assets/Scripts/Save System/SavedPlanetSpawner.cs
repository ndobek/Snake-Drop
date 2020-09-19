﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlanetSpawner : MonoBehaviour
{
    public GameObject planetViewer;
    public int quantity;
    public float xDistance;
    public bool excludeHighScore;

    private void Awake()
    {
        LoadAll();
    }

    public void LoadAll()
    {
        string[] saves = SaveManager.GetAllSaveNames();
        quantity = saves.Length;
        for (int i = 0; i < quantity; i++)
        {
            if (!(excludeHighScore && saves[i] == SaveManager.HighScoreSaveName))
            {
                GameObject spawned = Instantiate(planetViewer, transform);
                spawned.transform.position = new Vector3(xDistance * i, spawned.transform.position.y);
                spawned.GetComponent<SavedPlanetViewer>().LoadPlanet(saves[i]);
            }
        }
    }

}