using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimGrow : MonoBehaviour, IGrowable
{

    public int xp = 0;
    public int xpPerGrow = 5;

    public Animator animator;


    private int growthStage;
    public int GrowthStage
    {
        get { return growthStage; }
        set
        {
            growthStage = value;
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
        // Debug.Log("xp = " + xp);

    }
    public void UpdateGrowth()
    {
        if (ShouldGrow()) Grow();
        animator.SetInteger("Growth Stage", growthStage);
    }
}
