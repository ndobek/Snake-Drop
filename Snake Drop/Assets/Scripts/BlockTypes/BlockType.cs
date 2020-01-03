using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;
    public BlockType ChangeTypeToOnDeath;
    public int FallDestroyThreshold;

    #region Permissions

    protected virtual bool CanMoveToWithoutCrashing(Block block, BlockSlot slot)
    {
        bool CheckIfSnake()
        {
            if (slot && slot.Block && block && block.Tail)
            {
                return slot.Block.isPartOfSnake;
            }
            return false;
        }
        return
            slot != null &&
            GameManager.instance.HeightLimitIndicator.CheckHeightLimit(slot) &&
            !CheckIfSnake();
    }
    public virtual bool CanBasicMoveTo(Block block, BlockSlot slot)
    {
        return
            CanMoveToWithoutCrashing(block, slot) &&
            slot.Block == null;
    }
    protected virtual bool CanAction(Block block, BlockSlot slot)
    {
        if (slot && slot.Block)
        {
            return slot.Block.blockColor == block.blockColor;
        }
        return false;
    }
    public virtual bool OverrideCrash(Block block, BlockSlot slot)
    {
        bool result = false;
        if (slot && slot.Block && block && block.Tail)
        {
            result = slot.Block == block.Tail;
        }
        return result;
    }

    #endregion

    #region Actions

    protected virtual void Action(Block block, BlockSlot slot)
    {
        if (CanAction(block, slot))
        {
            Block tail = null;
            //BlockSlot oldLocation = block.Slot;
            slot.Block.KillSnake();
            slot.Block.ActionBreak();
            block.BasicMoveTo(slot);
            if (block.Tail) { tail = block.Tail; }

            block.ActionBreak();
            if (tail)
            {
                tail.BasicMoveTo(slot);
            }
            else
            {
                Crash(block, slot);
            }

        }
    }
    public virtual void OnActionFall(Block block)
    {

        BlockSlot NextBlock = block.Neighbor(GameManager.Direction.UP);
        bool NextBlockIsValid()
        {
            return NextBlock
                && NextBlock.Block
                && NextBlock.playGrid == block.Slot.playGrid;
        }
        BlockSlot OldLocation = block.Slot;

        block.BasicFall();
        BlockSlot FallenOnto = block.Neighbor(GameManager.Direction.DOWN);

        if (OldLocation.y - block.Slot.y >= FallDestroyThreshold && CanAction(block, FallenOnto))
        {
            Action(block, FallenOnto);
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
        if (CanMoveToWithoutCrashing(block, slot))
        {
            if (CanAction(block, slot))
            {
                Action(block, slot);
            }
            else if (CanBasicMoveTo(block, slot))
            {
                block.BasicMoveTo(slot);
            }
            else
            {
                Crash(block, slot);
            }
        }
        else
        {
            Crash(block, slot);
        }
    }
    public virtual void OnActionBreak(Block block)
    {
        GameManager.instance.difficultyManager.Score += 1;

        if (block == GameManager.instance.playerController.SnakeHead)
        {
            if (block.Tail)
            {
                GameManager.instance.playerController.SnakeHead = block.Tail;
            }
            else
            {
                GameManager.instance.playerController.SnakeHead = null;
                Crash(block, block.Slot);
            }
        }
        if (ChangeTypeToOnDeath) block.SetBlockType(block.blockColor, ChangeTypeToOnDeath);
    }

    #endregion

    //Tell the GameManager to end the round
    protected void Crash(Block block, BlockSlot slot)
    {
        if (!OverrideCrash(block, slot))
        {
            block.KillSnake();
            GameManager.instance.OnCrash();
        }
    }
}
