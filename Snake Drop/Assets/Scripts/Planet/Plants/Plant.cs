﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Plant : MonoBehaviour, IComparable<Plant>, ISaveable
{
    public string plantName;
    public string Name
    {
        get { return plantName; }
        set { plantName = value; }
    }
    public IGrowable growable;
    public ISunReactive sunReactive;
    public IWindReactive windReactive;
    [HideInInspector]
    public int xp = 0;
    
    void Awake()
    {
        InitializePlant();
    }

    public void InitializePlant()
    {
        growable = this.gameObject.GetComponent<IGrowable>();
        sunReactive = this.gameObject.GetComponent<ISunReactive>();
        windReactive = this.gameObject.GetComponent<IWindReactive>();
    }
    public void UpdatePlant()
    {
        if (growable != null) growable.UpdateGrowth();
        if (sunReactive != null) sunReactive.UpdateSunReaction();
        if (windReactive != null) windReactive.UpdateWindReaction();
    }

    public void AddXP(int xp)
    {
        if (growable != null) growable.AddXP(xp);
    }
    public int CompareTo(Plant plant)
    {
        return ComparePlants(this, plant);
    }

    public static int ComparePlants(Plant plant1, Plant plant2)
    {
        if (plant1.growable.GrowthStage < plant2.growable.GrowthStage) return -1;
        if (plant1.growable.GrowthStage > plant2.growable.GrowthStage) return 1;
        return 0;
    }

    public PlantSaveData SavePlant(Transform relativeTo = null)
    {
        PlantSaveData result = new PlantSaveData();

        result.position = gameObject.transform.position;
        result.rotation = gameObject.transform.rotation;

        if(relativeTo != null)
        {
            result.position = relativeTo.InverseTransformPoint(result.position);
            result.rotation *= Quaternion.Inverse(relativeTo.rotation);
        }

        result.growthStage = growable.GrowthStage;

        result.plantName = plantName;

        return result;
    }

    public void LoadPlant(PlantSaveData obj)
    {
        gameObject.transform.localPosition = obj.position;
        gameObject.transform.localRotation = obj.rotation;

        growable.GrowthStage = obj.growthStage;
    }

}
