using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Blocks/BlockTypes/MoveRules/SmashButYouCanEatYourSnake")]
public class SmashAndYouCanEatYourSnakeMove : MoveRule
{
    protected override bool MoveCondition(Block block, BlockSlot slot)
    {
        if (slot && slot.Block)
        {
            //if (slot.Block && slot.Block.isPartOfSnake) return false;
            return slot.Block.blockColor == block.blockColor;
        }
        return false;
    }
    protected override void MoveAction(Block block, BlockSlot slot)
    {
        Block tail = null;
        if (block.Tail) { tail = block.Tail; }
        if (slot.Block && slot.Block.isPartOfSnake) slot.Block.KillSnake();
        slot.Block.Break();
        block.RawMoveTo(slot);
        block.Break();
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
