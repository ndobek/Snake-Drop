using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Plant : MonoBehaviour, IComparable<Plant>
{
    public IGrowable growable;
    public IWindReactor windReactive;
    public ISunReactor sunReactive;

    private void Awake()
    {
        growable = gameObject.GetComponent<IGrowable>();
        windReactive = gameObject.GetComponent<IWindReactor>();
        sunReactive = gameObject.GetComponent<ISunReactor>();
    }
    public void AddXP(int xp)
    {
        if(growable != null) growable.AddXP(xp);
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

    public void UpdatePlant()
    {
        if(growable != null) growable.UpdateGrowable();
        if (windReactive != null) windReactive.UpdateWind();
        if (sunReactive != null) sunReactive.UpdateSun();
    }

}
