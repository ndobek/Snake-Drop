using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Containers And Conditions/Selector")]
public class FR_Select : FloatRule
{
    [System.Serializable]
    public struct selection
    {
        public FloatRule rule;
        public float probability;
    }

    public selection[] possibleSelections;

    protected override float Action(PlayerManager player = null)
    {
        float denominator = 0;
        foreach (var selection in possibleSelections)
        {
            denominator += selection.probability;
        }
        float result = Random.Range(0, denominator);

        foreach (var selection in possibleSelections)
        {
            result -= selection.probability;
            if (result <= 0) return selection.rule.Invoke(player);
        }
        return possibleSelections[possibleSelections.Length - 1].rule.Invoke(player);
    }
}
