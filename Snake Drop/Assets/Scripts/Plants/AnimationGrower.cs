using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGrower : MonoBehaviour, IGrowable
{
    public Animator animator;

    public void ResetGrowth() { }
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
        GrowthStage += 1;
    }


}
