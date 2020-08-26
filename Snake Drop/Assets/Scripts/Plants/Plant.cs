using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Plant : MonoBehaviour, IComparable<Plant>, IGrowable, ILatitudeDependent, IWindReactive, ISunReactive
{
    Animator anim;
    public Latitude lat;
    public Sun sun;
    public Wind wind;

    public IGrowable growable;
    [HideInInspector]
    public int xp = 0;
    public int xpPerGrow = 5;

    float latitude;

    public void AnimUpdate()
    {
        Gust();
        GustStrength();
        SunZenith();
    }
    public void Gust()
    {
        anim.SetFloat("Gust", wind.Gust(latitude));
    }

    public void GustStrength()
    {
        anim.SetFloat("Gust Strength", wind.GustStrength(latitude));
    }
    public void SunZenith()
    {
        anim.SetFloat("Sun Zenith", sun.SunZenith(latitude));
    }

    public float ReturnLatitude()
    {
        float result = lat.GetLatitude(this.gameObject.transform.position);
        return result;
    }
    public bool ShouldGrow()
    {
        return xp >= xpPerGrow;
    }
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
        xp = 0;
        GrowthStage += 1;
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

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        growable = gameObject.GetComponent<IGrowable>();

        latitude = ReturnLatitude();
    }
}
