using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlantViewer : MonoBehaviour
{
    private Plant plant;

    private void Awake()
    {
        plant = gameObject.GetComponentInChildren<Plant>();
    }
    private void Update()
    {
        plant.UpdatePlant();
    }
}
