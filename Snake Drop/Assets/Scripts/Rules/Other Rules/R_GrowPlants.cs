using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/GrowPlants")]
public class R_GrowPlants : Rule
{
    public int GrowthAmount;

    protected override void Action(Block block, PlayerManager player = null)
    {
        GameManager.instance.plantManager.AddXP(GrowthAmount);
    }
}
