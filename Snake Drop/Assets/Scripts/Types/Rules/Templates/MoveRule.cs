using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveRule : ScriptableObject
{
    public BlockAnimator Animation;
    protected virtual bool UniversalRules(Block block, BlockSlot slot, PlayerManager player = null)
    {
        if (slot == null) return false;
        if (player && !player.HeightLimitIndicator.CheckHeightLimit(slot)) return false;
        return true;
    }
    public bool CanMoveTo(Block block, BlockSlot slot, PlayerManager player = null)
    {
        return UniversalRules(block, slot, player) && MoveCondition(block, slot, player);
    }
    public void OnMove(Block block, BlockSlot slot, PlayerManager player = null)
    {
        block.Slot.playGrid.SetDirty();
        if (CanMoveTo(block, slot, player)) MoveAction(block, slot, player);
    }
    protected abstract bool MoveCondition(Block block, BlockSlot slot, PlayerManager player = null);
    protected abstract void MoveAction(Block block, BlockSlot slot, PlayerManager player = null);


}
