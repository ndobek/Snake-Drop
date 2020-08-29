﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Plant : MonoBehaviour, IComparable<Plant>
{
    public IGrowable growable;
    public ISunReactive sunReactive;
    public IWindReactive windReactive;
    [HideInInspector]
    public int xp = 0;
    public int xpPerGrow = 5;

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

}
