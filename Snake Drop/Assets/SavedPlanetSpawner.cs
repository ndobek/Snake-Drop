using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlanetSpawner : MonoBehaviour
{
    public GameObject planetViewer;

    public float xDistance;

    private void Awake()
    {
        LoadAll();
    }

    public void LoadAll()
    {
        List<SaveData> saves = SaveManager.LoadAll();

        for (int i = 0; i < saves.Count; i++)
        {
            GameObject spawned = Instantiate(planetViewer, transform);
            spawned.transform.position = new Vector3(xDistance * i, spawned.transform.position.y);
            spawned.GetComponent<SavedPlanetViewer>().LoadPlanet(saves[i]);
        }
    }

}
