using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Types/SlotType")]
public class SlotType : ScriptableObject 
{
    public Rule[] AssignmentRules;
    public Rule[] UnassignmentRules;

    public void OnAssignment(Block block)
    {
        foreach(Rule rule in AssignmentRules)
        {
            rule.Invoke(block);
        }
    }
    public void OnUnassignment(Block block)
    {
        foreach (Rule rule in UnassignmentRules)
        {
            rule.Invoke(block);
        }
    }
}
