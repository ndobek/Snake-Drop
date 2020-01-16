using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Types/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;
    public int sortingOrder;
    public MoveRule[] moveRules;
    public Rule[] killRules;
    public Rule[] breakRules;
    public Rule[] fallRules;
    //public int FallDestroyThreshold;

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
            for (int r = moveRules.Length - 1; r >= 0; r--)
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

    public virtual void OnFall(Block block)
    {
        foreach (Rule rule in fallRules)
        {
            if (block && rule) rule.Invoke(block);
        }
    }

    public virtual void OnKill(Block block)
    {
        foreach (Rule rule in killRules)
        {
            if (block && rule) rule.Invoke(block);
        }
    }

    public virtual void OnBreak(Block block)
    {
        foreach (Rule rule in breakRules)
        {
            if (block && rule) rule.Invoke(block);
        }
    }

    #endregion


}
