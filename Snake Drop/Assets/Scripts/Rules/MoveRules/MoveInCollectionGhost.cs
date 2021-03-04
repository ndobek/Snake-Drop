using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Rules/MoveRule/MoveIntoCollectionGhost")]
public class MoveInCollectionGhost : MoveRule
{
    public BlockType CollectionGhostMemberType;
    public bool AbsorbFromTail;
    public float blockFillAnimationSpeed;
    public Rule OnBreak;

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
            slot.Block.BlockCollection.IncreaseFillAmmount(player);
            block.RawMoveTo(slot);
            if (block.Tail)
            {
                block.Tail.RawMoveTo(slot);
            }
            if (OnBreak != null) OnBreak.Invoke(block, player);
            block.Break(player);

        } else if(block.blockColor != slot.Block.blockColor && !CollectionIsFull(slot.Block))
        {
            if (block.Tail && AbsorbFromTail) RemoveFirstColorInTail(block, slot, player);
            block.RawMoveTo(slot);
        }
        else
        {
            block.RawMoveTo(slot);
        }

        Debug.Log("Fill: " + slot.Block.BlockCollection.FillAmount + "/" + slot.Block.BlockCollection.Area());
    }

    private void RemoveFirstColorInTail(Block block, BlockSlot slot, PlayerManager player)
    {
        if (block.Tail && !CollectionIsFull(slot.Block) && slot.Block.BlockCollection.CoordsAreInCollection(block.X, block.Y))
        {
            if(block.Tail.blockColor == slot.Block.blockColor)
            {
                slot.Block.BlockCollection.IncreaseFillAmmount(player);
                Block obj1 = block.Tail;
                Block obj2 = block.Tail.Tail;

                block.SetTail(obj2);
                obj1.SetTail(null);

                if(obj2) obj2.RawMoveTo(obj1.Slot);
                obj1.RawMoveTo(block.Slot);
                if (OnBreak != null) OnBreak.Invoke(obj1, player);
                obj1.RawBreak();
            }
            else
            {
                RemoveFirstColorInTail(block.Tail, slot, player);
            }
        }
    }

    private bool CollectionIsFull(Block block)
    {
        return block.BlockCollection.isFull();
    }



}
