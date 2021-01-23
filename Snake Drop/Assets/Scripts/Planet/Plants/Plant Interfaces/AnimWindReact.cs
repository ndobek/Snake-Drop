using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimWindReact : MonoBehaviour,  IWindReactive
{
    [HideInInspector]
    public Animator anim;
    public Wind wind;
    public string WindSpeedAnimatorVar = "Wind Speed";

    public void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    public void UpdateWindReaction()
    {
        if (anim != null) anim.SetFloat(WindSpeedAnimatorVar, GetWindSpeed());
    }

    private float GetWindSpeed()
    {
        return wind.GetWindSpeed(transform);
    }
}
