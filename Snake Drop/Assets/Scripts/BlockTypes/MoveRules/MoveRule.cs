using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveRule : ScriptableObject
{
    protected virtual bool UniversalRules(Block block, BlockSlot slot)
    {
        if (slot == null) return false;
        if (!GameManager.instance.HeightLimitIndicator.CheckHeightLimit(slot)) return false;
        return true;
    }
    public bool CanMoveTo(Block block, BlockSlot slot)
    {
        return MoveCondition(block, slot) && UniversalRules(block, slot);
    }
    public void OnMove(Block block, BlockSlot slot)
    {
        if (CanMoveTo(block, slot)) MoveAction(block, slot);
    }
    protected abstract bool MoveCondition(Block block, BlockSlot slot);
    protected abstract void MoveAction(Block block, BlockSlot slot);


}
