﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PlantAnimator : MonoBehaviour, IGrowable
//{
//    //This script should go on the plants.
  
//    float sunMovementValue;
//    float windMovementValue;
//    float windSpeedValue;
//    public float offset;

//    Animator anim;
//    public WaveData waveData;
//    public void ResetGrowth() { GrowthStage = 0; }

//    private int growthStage;
//    public int GrowthStage
//    {
//        get { return growthStage; }
//        set
//        {
//            growthStage = value;
//            anim.SetInteger("Growth Stage", growthStage);
//        }
//    }

//    public void Grow()
//    {
//        GrowthStage += 1;
//    }
//    private void Awake()
//    {
     
//        offset = waveData.GetMovementOffset(gameObject.transform.position);
//        Debug.Log(offset);
//        anim = gameObject.GetComponent<Animator>();
       
//    }

//    private void Update()
//    {
        
//        sunMovementValue = MovementWave.AccessMovementWave(waveData.sunRate, offset);
//        windMovementValue = MovementWave.AccessMovementWave(waveData.windRate, offset);
//        windSpeedValue = MovementWave.AccessMovementWave(waveData.windSpeedRate);
//        anim.SetFloat("Sun", sunMovementValue);
//        anim.SetFloat("Wind", windMovementValue);
//        anim.SetFloat("Wind Speed", windSpeedValue);
//    }
//}
