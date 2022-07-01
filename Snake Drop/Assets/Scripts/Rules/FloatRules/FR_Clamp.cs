using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Clamp")]
public class FR_Clamp : FloatRule
{
    public FloatRule input;
    public FloatRule min;
    public FloatRule max;

    protected override float Action(PlayerManager player)
    {
        return Mathf.Clamp(input.Invoke(player), min.Invoke(player), max.Invoke(player));
    }
}
