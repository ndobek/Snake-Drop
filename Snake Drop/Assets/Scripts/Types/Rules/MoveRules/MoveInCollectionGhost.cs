using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Rules/MoveRule/MoveIntoCollectionGhost")]
public class MoveInCollectionGhost : MoveRule
{
    public BlockType CollectionGhostMemberType;

    protected override bool MoveCondition(Block block, BlockSlot slot, PlayerManager player = null)
    {

        return slot.Block &&
            slot.Block.blockType == CollectionGhostMemberType && 
            slot.Blocks.TrueForAll(Block.isNotPartOfSnake);
    }

    protected override void MoveAction(Block block, BlockSlot slot, PlayerManager player = null)
    {
        if (block.blockColor == slot.Block.blockColor && !CollectionIsFull(slot.Block))
        {
            IncreaseFillAmmount(slot.Block);
            block.RawMoveTo(slot);
            if (block.Tail)
            {
                //block.Tail.RawMoveTo(block.Slot);
                block.Tail.RawMoveTo(slot);
            }
            block.Break(player);

        } else if(block.blockColor != slot.Block.blockColor && !CollectionIsFull(slot.Block))
        {
            if(block.Tail) RemoveFirstColorInTail(block, slot);
            block.RawMoveTo(slot);
        }
        else
        {
            block.RawMoveTo(slot);
        }

        Debug.Log("Fill: " + slot.Block.BlockCollection.FillAmount + "/" + slot.Block.BlockCollection.Area());
    }

    private void RemoveFirstColorInTail(Block block, BlockSlot slot)
    {
        if (block.Tail && !CollectionIsFull(slot.Block) && slot.Block.BlockCollection.CoordsAreInCollection(block.X, block.Y))
        {
            if(block.Tail.blockColor == slot.Block.blockColor)
            {
                IncreaseFillAmmount(slot.Block);
                Block obj1 = block.Tail;
                Block obj2 = block.Tail.Tail;

                block.SetTail(obj2);
                obj1.SetTail(null);

                if(obj2) obj2.RawMoveTo(obj1.Slot);
                obj1.RawBreak();
            }
            else
            {
                RemoveFirstColorInTail(block.Tail, slot);
            }
        }
    }

    private bool CollectionIsFull(Block block)
    {
        return block.BlockCollection.isFull();
    }
    private void IncreaseFillAmmount(Block block)
    {
        block.BlockCollection.FillAmount += 1;
    }
}
