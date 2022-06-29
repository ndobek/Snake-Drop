using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Float Value")]
public class FR_Value : FloatRule
{
    public float value;
    protected override float Action(float input, PlayerManager player = null)
    {
        return value;
    }
}