using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BreakRules/CollectionSingleRule")]
public class CollectionBreakRule : BreakRule
{
    public BreakRule breakRule;

    protected override void BreakAction(Block block)
    {
        Block[] blocks = block.BlockCollection.Blocks;

        foreach (Block obj in blocks)
        {
            breakRule.OnBreak(obj);
        }
    }
}
