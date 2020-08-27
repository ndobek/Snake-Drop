using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGrower : MonoBehaviour, IGrowable
{
    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public int xp = 0;
    public int xpPerGrow = 5;

    public void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    public void ResetGrowth() { GrowthStage = 0; }

    private int growthStage;
    public int GrowthStage
    {
        get { return growthStage; }
        set
        {
            growthStage = value;
            animator.SetInteger("Growth State", growthStage);
        }
    }

    public void Grow()
    {
        xp = 0;
        GrowthStage += 1;
    }

    public bool ShouldGrow()
    {
        return xp >= xpPerGrow;
    }

    public void AddXP(int XP)
    {
        xp += XP;
    }

    public void UpdateGrowable() 
    {
        if (ShouldGrow()) Grow();
    }
}
