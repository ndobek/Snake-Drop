using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimGrow : MonoBehaviour, IGrowable
{
    Animator anim;

    [HideInInspector]
    public int xp = 0;
    public int xpPerGrow = 5;
    public void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
 

    private int growthStage;
    public int GrowthStage
    {
        get { return growthStage; }
        set
        {
            growthStage = value;
            if (anim != null) anim.SetInteger("Growth Stage", growthStage);
        }
    }
    public bool ShouldGrow()
    {
        return xp >= xpPerGrow;
    }
    public void Grow()
    {
        xp = 0;
        GrowthStage += 1;
    }
    public void ResetGrowth() { GrowthStage = 0; }
    public void AddXP(int XP)
    {
        xp += XP;
        Debug.Log("xp = " + xp);

    }
    public void UpdateGrowth()
    {
        if (ShouldGrow()) Grow();
    }
}
