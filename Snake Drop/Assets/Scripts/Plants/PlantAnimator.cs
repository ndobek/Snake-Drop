using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimator : MonoBehaviour, IGrowable
{
    //This script should go on the plants.
  
    float sunMovementValue;
    float windMovementValue;
    float offset;

    PlantMovement plantMovement;
    Animator anim;
    public void ResetGrowth() { GrowthStage = 0; }

    private int growthStage;
    public int GrowthStage
    {
        get { return growthStage; }
        set
        {
            growthStage = value;
            anim.SetInteger("Growth Stage", growthStage);
        }
    }

    public void Grow()
    {
        GrowthStage += 1;
    }
    private void Awake()
    {
        plantMovement = GameObject.Find("PlantMovement").GetComponent<PlantMovement>();
        offset = plantMovement.GetMovementOffset(gameObject.transform.position);
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        
        sunMovementValue = plantMovement.AccessPlantMovementWave(plantMovement.sunRate, offset);
        windMovementValue = plantMovement.AccessPlantMovementWave(plantMovement.windRate, offset);
        anim.SetFloat("Sun", sunMovementValue);
        anim.SetFloat("Wind", windMovementValue);
    }
}
