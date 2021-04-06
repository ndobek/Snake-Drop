//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AnimSunReact : MonoBehaviour, ISunReactive
//{
//    [HideInInspector]
//    public Animator anim;
//    public string SunAngleAnimatorVar = "Sun Angle";

//    [HideInInspector]
//    public Transform SunLocation;

//    public void Awake()
//    {
//        anim = gameObject.GetComponent<Animator>();
//    }

//    public void UpdateSunReaction()
//    {
//        if (anim != null && SunLocation) anim.SetFloat(SunAngleAnimatorVar, GetAngle());
//    }

//    private float GetAngle()
//    {
//        return Mathf.Atan2(SunLocation.position.y - transform.position.y, SunLocation.position.x - transform.position.x);
//    }
//}
