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
        block.ActionMove(GameManager.Direction.DOWN);

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


    public virtual void OnActionMove(Block block, BlockSlot slot)
    {
        bool CanActionMoveTo()
        {
            Block eatenBlock = null;
            if (slot)
            {
                eatenBlock = slot.Block;

                return
                    (eatenBlock == null || eatenBlock.blockColor == block.blockColor) &&
                    slot.y <= GameManager.instance.HeightLimitIndicator.HeightLimit;
            }

            return false;
        }

        if (CanActionMoveTo())
        {
            if (slot.Block)
            {
                slot.Block.KillSnake();
                slot.Block.ActionBreak();

                if (block.Tail)
                {
                    GameManager.instance.playerController.SnakeHead = block.Tail;
                    //block.Tail.BasicMoveTo(block.Slot);
                }
                else
                {
                    GameManager.instance.OnSnakeDeath();
                }

                block.ActionBreak();
            }
            else
            {

                block.BasicMoveTo(slot);
            }


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
