using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/RandomRange")]
public class FR_RandomRange : FloatRule
{
    public float min;
    public float max;

    protected override float Action(float input, PlayerManager player)
    {
        return Random.Range(min, max);
    }
}
