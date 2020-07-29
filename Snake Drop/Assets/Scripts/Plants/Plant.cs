using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IComparable<Plant>
{
    public IGrowable growable;

    [HideInInspector]
    public int xp = 0;
    public int xpPerGrow = 5;

    public bool CurrentlyGrowing;

    public void AddXP(int amount)
    {
        if (CurrentlyGrowing) xp += amount;
    }

    private void Awake()
    {
        growable = gameObject.GetComponent<IGrowable>();
    }

    private void Update()
    {
        if (ShouldGrow()) Grow();
    }

    public bool ShouldGrow()
    {
        return xp >= xpPerGrow;
    }
    public void Grow()
    {
        xp = 0;
        growable.Grow();
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
