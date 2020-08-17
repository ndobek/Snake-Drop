using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimator : MonoBehaviour
{
    //This script should go on the plants.
    //TODO: make this script give its movement values to the animator, make the animator use those numbers to change the state
    float sunMovementValue;
    float windMovementValue;
    float offset;

    PlantMovement plantMovement;
    private void Awake()
    {
        plantMovement = GameObject.Find("PlantMovement").GetComponent<PlantMovement>();
        offset = plantMovement.GetMovementOffset(gameObject.transform.position);
        
    }

    private void Update()
    {
        
        sunMovementValue = plantMovement.AccessPlantMovementWave(plantMovement.sunRate, offset);
        windMovementValue = plantMovement.AccessPlantMovementWave(plantMovement.windRate, offset);
    }
}
