using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSunReact : MonoBehaviour, ISunReactive
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SunData sun;
    public ILatitudeDependent l;
    [HideInInspector]
    public float latitude;

    public void Awake()
    {
        sun = GameObject.Find("SunData").GetComponent<SunData>();
        anim = gameObject.GetComponent<Animator>();
        l = gameObject.GetComponent<ILatitudeDependent>();
    }
    public void Start()
    {
        latitude = l.ReturnLatitude();
    }

    public void UpdateSunReaction()
    {
        if (anim != null) anim.SetFloat("Sun Zenith", sun.SunZenith(latitude));
    }
}
