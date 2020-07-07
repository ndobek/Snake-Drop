using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Containers And Conditions/Rule Group")]
public class R_RuleGroup : Rule
{
    public Rule[] Rules;
    protected override void Action(Block block, PlayerManager player = null)
    {
        foreach(Rule rule in Rules)
        {
            rule.Invoke(block, player);
        }
    }
}
