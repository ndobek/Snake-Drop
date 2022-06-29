using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Containers And Conditions/Rule Group")]
public class FR_RuleGroup : FloatRule
{
    public FloatRule[] Rules;
    protected override float Action(float input, PlayerManager player = null)
    {
        float result = input;
        foreach (FloatRule rule in Rules)
        {
            result = rule.Invoke(result, player);
        }
        return result;
    }
}