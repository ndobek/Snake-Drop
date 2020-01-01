using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;
    public BlockType ChangeTypeToOnDeath;
    public int FallDestroyThreshold;

    //public virtual bool CanActionMove(Block block, BlockSlot slot)
    //{
    //    return true;
    //}

    public virtual void OnActionFall(Block block)
    {

        BlockSlot NextBlock = block.Neighbor(GameManager.Direction.UP);
        BlockSlot OldLocation = block.Slot;

        bool NextBlockIsValid()
        {
            return NextBlock
                && NextBlock.Block
                && NextBlock.playGrid == block.Slot.playGrid;
        }

        block.BasicFall();
        BlockSlot FallenOnto = block.Neighbor(GameManager.Direction.DOWN);

        if (OldLocation.y - block.Slot.y >= FallDestroyThreshold && CanActionMoveTo(block, FallenOnto))
        {
            OnActionMove(block, FallenOnto);

            //if (NextBlockIsValid()) OnActionFall(NextBlock.Block);
        }
        else
        {
            while (NextBlockIsValid())
            {
                BlockSlot Current = NextBlock;
                NextBlock = Current.GetNeighbor(GameManager.Direction.UP);
                Current.Block.BasicFall();
            }
        }

    }

    private bool CanActionMoveTo(Block block, BlockSlot slot)
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

    public virtual void OnActionMove(Block block, BlockSlot slot)
    {
        if (CanActionMoveTo(block, slot))
        {
            if (slot.Block)
            {
                GameManager.instance.difficultyManager.Score += 1;
                slot.Block.Break();
                //block.Tail.BasicMoveTo(block.Slot);
                if (block.Tail)
                {
                    GameManager.instance.playerController.SnakeHead = block.Tail;
                }
                else
                {
                    GameManager.instance.OnSnakeDeath();
                }

                block.Break();
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


    public virtual void OnKill(Block block)
    {
        if (block.Tail != null)
        {
            if (block.Tail.Slot.playGrid == GameManager.instance.playGrid) block.Tail.Kill();
            block.SetTail(null);
        }
        block.isPartOfSnake = false;
        if(ChangeTypeToOnDeath) block.SetBlockType(block.blockColor, ChangeTypeToOnDeath);
        block.UpdateBlock();
    }
}
