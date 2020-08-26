using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [HideInInspector]
    public PlantSpawner[] spawners;


    public int DefaultNumberOfPlantsPerSpawn = 1;

    public Plant[] currentlyGrowing;

    public float MaximumDistanceFromPoint;


    private void Awake()
    {
        spawners = GetComponentsInChildren<PlantSpawner>();

    }
    public PlantSpawner GetRandSpawnerNearTo(Vector3 position)
    {
        List<PlantSpawner> possibleResults = GetAllPossibleSpawnersNearTo(position);

        if (possibleResults.Count > 0) return possibleResults[(int)Random.Range(0, possibleResults.Count)];
        else return null;
    }

    public List<PlantSpawner> GetAllPossibleSpawnersNearTo(Vector3 position)
    {
        List<PlantSpawner> results = new List<PlantSpawner>();
        foreach (PlantSpawner obj in spawners)
        {
            if (MaximumDistanceFromPoint == 0 || Vector3.Distance(obj.transform.position, position) < MaximumDistanceFromPoint)
            {
                results.Add(obj);
            }
        }
        return results;
    }

    public void AddXP(int amount)
    {
        foreach(Plant obj in currentlyGrowing)
        {
            if(obj) obj.xp += amount;
        }
    }
    private List<Plant> GetAllPossiblePlantsInSpawnerList(List<PlantSpawner> possibleSpawners)
    {
        List<Plant> result = new List<Plant>();
        foreach (PlantSpawner spawner in possibleSpawners)
        {
            result.AddRange(spawner.Plants);
        }
        return result;
    }

    public List<Plant> GetAllPossiblePlantsNearTo(Vector3 position)
    {
        return GetAllPossiblePlantsInSpawnerList(GetAllPossibleSpawnersNearTo(position));
    }

    public List<Plant> GetLeastGrownPlantsInPlantList(List<Plant> plants, int quantity)
    {
        List<Plant> result = new List<Plant>();
        plants.Sort();
        for (int i = 0; i < quantity && i < plants.Count; i++)
        {
            if (plants[i] != null) result.Add(plants[i]);
        }
        return result;
    }
    public List<Plant> GetRandPlantsFromList(List<Plant> plants, int quantity)
    {
        List<Plant> result = new List<Plant>();
        while(plants.Count > 0 && result.Count < quantity)
        {
            int i = Random.Range(0, plants.Count);
            result.Add(plants[i]);
            plants.RemoveAt(i);
        }
        return result;
    }

    public List<Plant> GetRandPlantsNearTo(Vector3 position, int quantity)
    {
        return GetRandPlantsFromList(GetAllPossiblePlantsNearTo(position), quantity);
    }

    public List<Plant> GetLeastGrownPlantsNearTo(Vector3 position, int quantity)
    {
        return GetLeastGrownPlantsInPlantList(GetAllPossiblePlantsNearTo(position), quantity);
    }

    public List<Plant> AllPlants()
    {
        return GetAllPossiblePlantsInSpawnerList(spawners.ToList());
    }

    public void ResetGrowth()
    {
        foreach(PlantSpawner obj in spawners )
        {
            obj.ResetPlants();
        }
    }
    public void PlantNewPlants(Vector3 position)
    {
        currentlyGrowing = GetRandPlantsNearTo(position, DefaultNumberOfPlantsPerSpawn).ToArray();
    }
    private void Update()
    {
        foreach (Plant plant in currentlyGrowing)
        {
            if (plant.ShouldGrow()) plant.Grow();
        }

        //Added this to animate the plants
        foreach (Plant plant in AllPlants())
        {
            plant.AnimUpdate();
        }
    }
}
