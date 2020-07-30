using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [HideInInspector]
    public PlantSpawner[] spawners;

    public int DefaultNumberOfPlantsPerSpawn = 1;

    public List<Plant> AllPlants;
    public Plant[] currentlyGrowing;

    public float MaximumDistanceFromPoint;


    private void Awake()
    {
        spawners = GetComponentsInChildren<PlantSpawner>();
    }
    public PlantSpawner GetRandSpawnerNearTo(Vector3 position)
    {
        List<PlantSpawner> possibleResults = new List<PlantSpawner>();
        foreach(PlantSpawner obj in spawners)
        {
            if(MaximumDistanceFromPoint == 0 || Vector3.Distance(obj.transform.position, position) < MaximumDistanceFromPoint)
            {
                possibleResults.Add(obj);
            }
        }

        if (possibleResults.Count > 0) return possibleResults[(int)Random.Range(0, possibleResults.Count)];
        else return null;
    }

    public void AddXP(int amount)
    {
        foreach(Plant obj in currentlyGrowing)
        {
            if(obj) obj.xp += amount;
        }
    }

    public void PlantNewPlants(Vector3 position, int amount)
    {
        Plant[] plants = new Plant[amount];
        for(int i = 0; i < amount; i++)
        {
            PlantSpawner spawner = GetRandSpawnerNearTo(position);
            if(spawner) plants[i] = spawner.Spawn().GetComponent<Plant>();
        }

        currentlyGrowing = plants;
        AllPlants.AddRange(plants.ToList());
    }

    public void PlantNewPlants(Vector3 position)
    {
        PlantNewPlants(position, DefaultNumberOfPlantsPerSpawn);
    }

    public void Reset()
    {
        foreach(Plant obj in AllPlants)
        {
            GameObject.Destroy(obj.gameObject);
        }
        AllPlants = new List<Plant>();
    }

}
