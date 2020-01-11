using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BreakRules/ChangeTypeRule")]
public class ChangeTypeRule : BreakRule
{
    public BlockType ChangeTypeToOnDeath;

    protected override void BreakAction(Block block)
    {
        if (ChangeTypeToOnDeath) block.SetBlockType(block.blockColor, ChangeTypeToOnDeath);
    }
}
