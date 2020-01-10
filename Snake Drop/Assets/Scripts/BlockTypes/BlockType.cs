using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;
    public MoveRule[] moveRules;
    public BlockType ChangeTypeToOnDeath;
    public int FallDestroyThreshold;

    #region Permissions

    public virtual bool CanMoveToWithoutCrashing(Block block, BlockSlot slot)
    {
        foreach(MoveRule rule in moveRules)
        {
            if (rule.CanMoveTo(block, slot)) return true;
        }
        return false;
    }
    public virtual bool OverrideMove(Block block, BlockSlot slot)
    {
        if (slot &&
            slot.Block &&
            block &&
            block.Tail &&
            slot.Block == block.Tail) return true;
        return false;
    }

    #endregion

    #region Actions

    public virtual void OnBasicFall(Block block)
    {
        BlockSlot destination = block.Neighbor(GameManager.Direction.DOWN);
        while (moveRules[0].CanMoveTo(block, destination) && destination.playGrid == block.Slot.playGrid)
        {
            OnMove(block, destination, 0);
            destination = block.Neighbor(GameManager.Direction.DOWN);
        }

        //    BlockSlot destination = block.Neighbor(GameManager.Direction.DOWN);
        //    while (destination != null &&
        //        destination.Block == null &&
        //        destination.playGrid == block.Slot.playGrid)
        //    {
        //        block.RawMoveTo(destination);
        //        destination = block.Neighbor(GameManager.Direction.DOWN);
        //    }
    }
    public virtual void OnActionFall(Block block)
    {
        block.BasicFall();

        //BlockSlot destination = block.Neighbor(GameManager.Direction.DOWN);
        //while (CanMoveToWithoutCrashing(block, destination))
        //{
        //    OnMove(block, destination);
        //    destination = block.Neighbor(GameManager.Direction.DOWN);
        //}

        //_____________________________________________________________
        //BlockSlot NextBlock = block.Neighbor(GameManager.Direction.UP);
        //bool NextBlockIsValid()
        //{
        //    return NextBlock
        //        && NextBlock.Block
        //        && NextBlock.playGrid == block.Slot.playGrid;
        //}
        //BlockSlot OldLocation = block.Slot;

        //block.BasicFall();
        //BlockSlot FallenOnto = block.Neighbor(GameManager.Direction.DOWN);

        //if (OldLocation.y - block.Slot.y >= FallDestroyThreshold && CanMoveToWithoutCrashing(block, FallenOnto))
        //{
        //    OnMove(block, FallenOnto);
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
    }

    public virtual void OnMove(Block block, BlockSlot slot, int moveType)
    {
        if (OverrideMove(block, slot)) return;

        moveRules[moveType].OnMove(block, slot);
    }
    public virtual void OnMove(Block block, BlockSlot slot)
    {
        if (OverrideMove(block, slot)) return;

        if (CanMoveToWithoutCrashing(block, slot))
        {
            for(int r = moveRules.Length - 1; r >= 0; r--)
            {
                if (moveRules[r].CanMoveTo(block, slot))
                {
                    moveRules[r].OnMove(block, slot);
                    break;
                }
            }
        }
        else
        {
            GameManager.instance.OnCrash();
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
                GameManager.instance.OnCrash();
            }
        }
        if (ChangeTypeToOnDeath) block.SetBlockType(block.blockColor, ChangeTypeToOnDeath);
    }

    #endregion


}
