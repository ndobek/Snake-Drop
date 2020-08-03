using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject[] PlantPrefabs;
    public int MaxPlants;
    [HideInInspector]
    public List<Plant> Plants;

    public float HorizontalVariance = 0;
    public float VerticalVariance = 0;

    private void Awake()
    {
        ResetPlants();
    }
    public void ResetPlants()
    {
        DestroyPlants();
        CreatePlants();
    }

    private void DestroyPlants()
    {
        foreach(Plant obj in Plants)
        {
            GameObject.Destroy(obj.gameObject);
        }
        Plants = new List<Plant>();
    }

    private void CreatePlants()
    {
        while (Plants.Count < MaxPlants)
        {
            Plants.Add(Spawn().GetComponent<Plant>());
            Plants.Sort();
        }
    }

    private GameObject Spawn()
    {
        GameObject PlantPrefab = PlantPrefabs[Random.Range(0, PlantPrefabs.Length)];
        GameObject result = Instantiate(PlantPrefab, transform);
        result.transform.localPosition = new Vector3(Random.Range(result.transform.localPosition.x - HorizontalVariance, result.transform.localPosition.x + HorizontalVariance) , Random.Range(result.transform.localPosition.y - VerticalVariance, result.transform.localPosition.y + VerticalVariance));

        return result;
    }
}
