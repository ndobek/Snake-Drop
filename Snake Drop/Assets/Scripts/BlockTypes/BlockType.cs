using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;
    public BlockType ChangeTypeToOnDeath;
    public int FallDestroyThreshold;

    public virtual void OnActionFall(Block block)
    {

        //BlockSlot NextBlock = block.Neighbor(GameManager.Direction.UP);
        //BlockSlot OldLocation = block.Slot;

        //bool NextBlockIsValid()
        //{
        //    return NextBlock
        //        && NextBlock.Block
        //        && NextBlock.playGrid == block.Slot.playGrid;
        //}

        //block.BasicFall();
        //BlockSlot FallenOnto = block.Neighbor(GameManager.Direction.DOWN);

        //if (OldLocation.y - block.Slot.y >= FallDestroyThreshold/* && CanActionMoveTo(block, FallenOnto)*/)
        //{
        //    block.ActionMove(GameManager.Direction.DOWN);

        //    //if (NextBlockIsValid()) OnActionFall(NextBlock.Block);
        //}
        //else
        //{
        //    while (NextBlockIsValid())
        //    {
        //        BlockSlot Current = NextBlock;
        //        NextBlock = Current.GetNeighbor(GameManager.Direction.UP);
        //        Current.Block.BasicFall();
        //    }
        //}

        block.BasicFall();
        BlockSlot FallenOnto = block.Neighbor(GameManager.Direction.DOWN);
        Action(block, FallenOnto);

    }

    //private bool CanActionMoveTo(Block block, BlockSlot slot)
    //{
    //    Block eatenBlock = null;
    //    if (slot)
    //    {
    //        eatenBlock = slot.Block;

    //        return 
    //            (eatenBlock == null || eatenBlock.blockColor == block.blockColor) && 
    //            slot.y <= GameManager.instance.HeightLimitIndicator.HeightLimit;
    //    }

    //    return false;
    //}    
    protected virtual bool CanAction(Block block, BlockSlot slot)
    {
        if (slot && slot.Block)
        {
            return slot.Block.blockColor == block.blockColor;
        }
        return false;
    }

    protected virtual void Action(Block block, BlockSlot slot)
    {
        if (CanAction(block, slot))
        {
            slot.Block.KillSnake();
            block.BasicMoveTo(slot);
            slot.Block.ActionBreak();
            if (block.Tail)
            {
                GameManager.instance.playerController.SnakeHead = block.Tail;
            }
            else
            {
                GameManager.instance.OnSnakeDeath();
            }

            block.ActionBreak();
        }
    }
    protected virtual bool CanBasicMoveTo(Block block, BlockSlot slot)
    {
        return 
            slot != null &&
            slot.y <= GameManager.instance.HeightLimitIndicator.HeightLimit &&
            slot.Block == null;
    }

    public virtual void OnActionMove(Block block, BlockSlot slot)
    {


        Debug.Log("CanBasicMoveTo()" + CanBasicMoveTo(block, slot));
        Debug.Log("CanAction()" + CanAction(block, slot));
        if (CanAction(block, slot))
        {
            Action(block, slot);
        } else if (CanBasicMoveTo(block, slot))
        {
            block.BasicMoveTo(slot);
        }
        else
        {
            block.KillSnake();
            GameManager.instance.OnSnakeDeath();
        }
    }


    public virtual void OnBreak(Block block)
    {
        GameManager.instance.difficultyManager.Score += 1;
        if (ChangeTypeToOnDeath) block.SetBlockType(block.blockColor, ChangeTypeToOnDeath);
    }
}
