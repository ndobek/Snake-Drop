using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Summary: This class provides animation data to the Animator controller on the plant.
//This script should go on the plants.
public class PlantAnimator : MonoBehaviour, IGrowable, ILatitudeDependent, IWindReactive, ISunReactive
{
    Animator anim;
    public Latitude lat;
    public Sun sun;
    public Wind wind;

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
        latitude = ReturnLatitude();
        anim = gameObject.GetComponent<Animator>();
    }

}
