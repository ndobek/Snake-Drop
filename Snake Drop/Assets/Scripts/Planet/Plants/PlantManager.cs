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


    /// <summary>
    /// Default number of plants per spawn.
    /// </summary>
    public int PlantsPerSpawn = 1;

    public Plant[] currentlyGrowing;

    /// <summary>
    /// Maximum distance from point.
    /// </summary>
    public float MaxDistance;

    public Plant[] allPlants;

    private void Awake()
    {
        spawners = GetComponentsInChildren<PlantSpawner>();
        
    }
    private void Start()
    {
        ResetGrowth();

    }
    #region Methods
    /// <summary>
    /// Returns a random PlantSpawner near to a Vector3 position.
    /// </summary>
    /// <param name="position">The Vector3 position to be checked.</param>
    /// <returns> PlantSpawner </returns>
    public PlantSpawner RandSpawnerNear(Vector3 position)
    {
        List<PlantSpawner> possibleResults = AllSpawnersNear(position);

        if (possibleResults.Count > 0) return possibleResults[(int)Random.Range(0, possibleResults.Count)];
        else return null;
    }
    
    /// <summary>
    /// Returns a list of all the possible PlantSpawners near to a Vector3 position.
    /// </summary>
    /// <param name="position">The Vector3 position to be checked.</param>
    /// <returns>List of type PlantSpawners.</returns>
    public List<PlantSpawner> AllSpawnersNear(Vector3 position)
    {
        List<PlantSpawner> results = new List<PlantSpawner>();
        foreach (PlantSpawner obj in spawners)
        {
            if (MaxDistance == 0 || Vector3.Distance(obj.transform.position, position) < MaxDistance)
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
            if(obj) obj.AddXP(amount);
        }
    }

    /// <summary>
    /// Returns a list of all the possible Plants in a spawner list.
    /// </summary>
    /// <param name="possibleSpawners">List of PlantSpawners to be checked.</param>
    /// <returns>List of type Plant.</returns>
    private List<Plant> AllPlantsIn(List<PlantSpawner> possibleSpawners)
    {
        List<Plant> result = new List<Plant>();
        foreach (PlantSpawner spawner in possibleSpawners)
        {
            result.AddRange(spawner.Plants);
        }
        return result;
    }

    /// <summary>
    /// Returns list of all possible Plants near to a Vector3 position.
    /// </summary>
    /// <param name="position">The Vector3 position to be checked.</param>
    /// <returns>List of type Plant.</returns>
    public List<Plant> AllPlantsNear(Vector3 position)
    {
        return AllPlantsIn(AllSpawnersNear(position));
    }

    /// <summary>
    /// Returns a list of the n least grown plants in a plant list.
    /// </summary>
    /// <param name="plants">The list of plants to be checked.</param>
    /// <param name="quantity">The integer quantity of plants to be returned. (n)</param>
    /// <returns>List of type Plant and length n.</returns>
    public List<Plant> LGPlantsIn(List<Plant> plants, int quantity)
    {
        List<Plant> result = new List<Plant>();
        plants.Sort();
        for (int i = 0; i < quantity && i < plants.Count; i++)
        {
            if (plants[i] != null) result.Add(plants[i]);
        }
        return result;
    }

    /// <summary>
    /// Returns a list of n random plants from a plant list.
    /// </summary>
    /// <param name="plants">The list of plants to be checked.</param>
    /// <param name="quantity">The integer quantity of plants to be returned. (n) </param>
    /// <returns>List of type Plant and length n.</returns>
    public List<Plant> RandPlantsIn(List<Plant> plants, int quantity)
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

    /// <summary>
    /// Returns a list of n random Plants near to a Vector3 position.
    /// </summary>
    /// <param name="position">The Vector3 position to be checked.</param>
    /// <param name="quantity">The integer quantity of plants to be returned. (n)</param>
    /// <returns>List of type Plant and length n.</returns>
    public List<Plant> RandPlantsNear(Vector3 position, int quantity)
    {
        return RandPlantsIn(AllPlantsNear(position), quantity);
    }

    /// <summary>
    /// Returns a list of the n least grown plants near to a Vector3 position.
    /// </summary>
    /// <param name="position">The Vector3 position to be checked.</param>
    /// <param name="quantity">The integer quantity of plants to be returned. (n)</param>
    /// <returns>List of type Plant and length n.</returns>
    public List<Plant> LGPlantsNear(Vector3 position, int quantity)
    {
        return LGPlantsIn(AllPlantsNear(position), quantity);
    }

    /// <summary>
    /// Returns a list of all the plants.
    /// </summary>
    /// <returns>List of type Plant.</returns>
    public List<Plant> AllPlants()
    {
        return AllPlantsIn(spawners.ToList());
    }

    public void ResetGrowth()
    {
        foreach(PlantSpawner obj in spawners )
        {
            obj.ResetPlants();
        }
        allPlants = AllPlants().ToArray();
    }
    public void PlantNewPlants(Vector3 position)
    {
        currentlyGrowing = RandPlantsNear(position, PlantsPerSpawn).ToArray();

    }
    #endregion

    public PlanetSaveData SavePlanet()
    {
        PlanetSaveData result = new PlanetSaveData();

        foreach (Plant plant in allPlants)
        {
            result.plantData.Add(plant.SavePlant(transform));
        }

        return result;
    }

    private void Update()
    {
        foreach (Plant plant in allPlants)
        {
            plant.UpdatePlant();
        }


    }
}
