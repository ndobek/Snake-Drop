using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject[] PlantPrefabs;
    public int MaxPlants;
    public List<Plant> Plants;

    public float HorizontalVariance = 0;
    public float VerticalVariance = 0;

    public void AddXP(int amount)
    {
        foreach(Plant obj in Plants)
        {
            obj.AddXP(amount);
        }
    }

    public List<Plant> GetPlants()
    {
        return GetPlants(0);
    }

    public List<Plant> GetPlants(int quantity = 0)
    {
        while (Plants.Count < quantity && Plants.Count < MaxPlants)
        {
            Plants.Add(Spawn());
        }

        Plants.Sort();
        List<Plant> result = new List<Plant>();

        int index = 0;
        while(result.Count < quantity && result.Count < MaxPlants)
        {
            if (Plants[index] != null) result.Add(Plants[index]);
        }

        return result;
    }

    private Plant Spawn()
    {
        GameObject PlantPrefab = PlantPrefabs[Random.Range(0, PlantPrefabs.Length)];
        GameObject result = Instantiate(PlantPrefab, transform);
        return result.GetComponent<Plant>();
    }
}
