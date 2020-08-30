using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimWindReact : MonoBehaviour,  IWindReactive
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public WindData wind;
    public ILatitudeDependent l;
    [HideInInspector]
    public float latitude;


    public void Awake()
    {
        wind = GameObject.Find("WindData").GetComponent<WindData>();
        anim = this.gameObject.GetComponent<Animator>();
        l = this.gameObject.GetComponent<ILatitudeDependent>();
    }
    public void Start()
    {
        latitude = l.ReturnLatitude();
    }

    public void Gust()
    {
        if (anim != null) anim.SetFloat("Gust", wind.Gust(latitude));
    }

    public void GustStrength()
    {
        if (anim != null) anim.SetFloat("Gust Strength", wind.GustStrength());
    }
    public void UpdateWindReaction()
    {
        Gust();
        GustStrength();
    }
}
