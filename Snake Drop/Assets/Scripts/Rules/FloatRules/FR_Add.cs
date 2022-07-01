using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Add")]
public class FR_Add : FloatRule
{
    public FloatRule[] floats;

    protected override float Action(PlayerManager player)
    {
        float result = 0;

        foreach (var f in floats)
        {
            result += f.Invoke(player);
        }
        return result;
    }
}
