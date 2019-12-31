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

        if (OldLocation.y - block.Slot.y >= FallDestroyThreshold
            //&& FallenOnto
            //&& block.blockColor == FallenOnto.Block.blockColor
            )
        {
            OnActionMove(block, FallenOnto);

            if (NextBlockIsValid()) OnActionFall(NextBlock.Block);
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

    public virtual void OnActionMove(Block block, BlockSlot slot)
    {
        Block eatenBlock = null;
        if (slot) eatenBlock = slot.Block;

        bool CanMove()
        {
            return slot | (eatenBlock != null && eatenBlock.blockColor != block.blockColor)
                && (slot.y <= GameManager.instance.HeightLimitIndicator.HeightLimit);
        }

        //if (blockObj) Debug.Log(blockObj.blockType);
        //else Debug.Log("null");

        if (!CanMove())
        {
            block.Kill();
            GameManager.instance.OnSnakeDeath(block);
        }
        else
        {
            if (eatenBlock)
            {

                GameManager.instance.difficultyManager.Score += 1;
                eatenBlock.Break();
                //block.Break();
            }
            block.BasicMoveTo(slot);

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
