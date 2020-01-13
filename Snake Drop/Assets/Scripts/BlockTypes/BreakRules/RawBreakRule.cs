using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BreakRules/RawBreakRule")]
public class RawBreakRule : BreakRule
{
    protected override void BreakAction(Block block)
    {
        block.RawBreak();
    }
}
