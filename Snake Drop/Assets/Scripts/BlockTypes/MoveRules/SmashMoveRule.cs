using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/MoveRules/Smash")]
public class SmashMoveRule : MoveRule
{
    protected override bool MoveCondition(Block block, BlockSlot slot)
    {
        if (slot && slot.Block)
        {
            return slot.Block.blockColor == block.blockColor;
        }
        return false;
    }
    protected override void MoveAction(Block block, BlockSlot slot)
    {
        Block tail = null;
        if (block.Tail) { tail = block.Tail; }
        //block.KillSnake();
        slot.Block.ActionBreak();
        block.RawMoveTo(slot);
        block.ActionBreak();
        if (tail)
        {
            tail.RawMoveTo(slot);
        }
        else
        {
            GameManager.instance.OnCrash();
        }

    }
}
