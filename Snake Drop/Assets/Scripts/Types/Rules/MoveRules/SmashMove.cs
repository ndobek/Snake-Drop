using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/MoveRule/Smash")]
public class SmashMove : MoveRule
{
    protected override bool MoveCondition(Block block, BlockSlot slot, PlayerManager player = null)
    {

        if (slot && slot.Block)
        {
            if (slot.Block && !slot.Blocks.TrueForAll(Block.isNotPartOfSnake)) return false;
            return slot.Block.blockColor == block.blockColor;
        }
        return false;
    }
    protected override void MoveAction(Block block, BlockSlot slot, PlayerManager player = null)
    {
        Block tail = null;
        if (block.Tail) { tail = block.Tail; }
        //if (slot.Block && slot.Block.isPartOfSnake) slot.Block.KillSnake();
        slot.Block.Break(player);
        block.RawMoveTo(slot);
        block.Break(player);
        if (tail)
        {
            tail.RawMoveTo(slot);
        }
        else
        {
            player.OnCrash();
        }

    }
}
