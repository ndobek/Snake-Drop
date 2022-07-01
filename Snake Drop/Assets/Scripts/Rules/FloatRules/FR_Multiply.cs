using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Multiply")]
public class FR_Multiply : FloatRule
{
    public FloatRule[] floats;

    protected override float Action(PlayerManager player)
    {
        float result = floats[0].Invoke(player);

        for(int i = 1; i < floats.Length; i++)
        {
            result *= floats[i].Invoke(player);
        }
        return result;
    }
}
