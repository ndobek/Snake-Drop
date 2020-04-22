using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Types/BlockType")]
public class BlockType : ScriptableObject
{
    [SerializeReference]
    public BlockSpriteController BlockSprite;

    public int sortingOrder;

    public bool isPartOfSnake;

    public MoveRule[] moveRules;
    public Rule[] killRules;
    public Rule[] breakRules;
    public Rule[] specialActionRules;

    #region Permissions

    public virtual bool CanMoveToWithoutCrashing(Block block, BlockSlot slot, PlayerManager player = null)
    {
        foreach(MoveRule rule in moveRules)
        {
            if (rule.CanMoveTo(block, slot, player)) return true;
        }
        return false;
    }
    public virtual bool OverrideMove(Block block, BlockSlot slot, PlayerManager player = null)
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
    public virtual void OnMove(Block block, BlockSlot slot, int moveType, PlayerManager player = null)
    {
        if (OverrideMove(block, slot, player)) return;

        moveRules[moveType].OnMove(block, slot, player);
    }
    public virtual void OnMove(Block block, BlockSlot slot, PlayerManager player = null)
    {
        if (OverrideMove(block, slot, player)) return;

        if (CanMoveToWithoutCrashing(block, slot, player))
        {
            for (int r = moveRules.Length - 1; r >= 0; r--)
            {
                if (moveRules[r].CanMoveTo(block, slot, player))
                {
                    moveRules[r].OnMove(block, slot, player);
                    break;
                }
            }
        }
        else
        {
            GameManager.instance.OnCrash();
        }

    }
    public virtual void SpecialAction(Block block, PlayerManager player = null)
    {
        foreach (Rule rule in specialActionRules)
        {
            if (block && rule) rule.Invoke(block, player);
        }
    }

    public virtual void OnKill(Block block, PlayerManager player = null)
    {
        foreach (Rule rule in killRules)
        {
            if (block && rule) rule.Invoke(block, player);
        }
    }

    public virtual void OnBreak(Block block, PlayerManager player = null)
    {
        foreach (Rule rule in breakRules)
        {
            if (block && rule) rule.Invoke(block, player);
        }
    }
    public virtual void UpdateSprite(Block block)
    {
        BlockSprite.UpdateSprite(block);
    }

    #endregion


}
