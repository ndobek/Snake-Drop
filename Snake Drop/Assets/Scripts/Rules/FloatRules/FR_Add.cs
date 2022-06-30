using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Add")]
public class FR_Add : FloatRule
{
    public bool takeInput;
    public FloatRule[] floats;

    protected override float Action(float input, PlayerManager player)
    {
        float result = 0;
        if(takeInput) result = input;

        foreach (var f in floats)
        {
            input += f.Invoke(input);
        }
        return result;
    }
}
