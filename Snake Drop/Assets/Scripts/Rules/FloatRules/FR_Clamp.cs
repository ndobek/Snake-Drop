using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Clamp")]
public class FR_Clamp : FloatRule
{
    public float min;
    public float max;

    protected override float Action(float input, PlayerManager player)
    {
        return Mathf.Clamp(input, min, max);
    }
}
