using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/MoveRules/Basic")]
public class BasicMoveRule : MoveRule
{
    protected override bool MoveCondition(Block block, BlockSlot slot)
    {
        return slot && slot.Block == null;
    }
    protected override void MoveAction(Block block, BlockSlot slot)
    {
        block.RawMoveTo(slot);
    }
}
