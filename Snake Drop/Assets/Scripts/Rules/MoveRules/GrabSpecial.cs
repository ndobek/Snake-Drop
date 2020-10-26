using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/MoveRule/GrabSpecial")]
public class GrabSpecial : MoveRule
{
    public GridAction DoToGridOnGrab;
    public Rule DoToSnakeOnGrab;

    protected override bool MoveCondition(Block block, BlockSlot slot, PlayerManager player = null)
    {

        if (slot && slot.Block)
        {
            if (slot.Block && !slot.Blocks.TrueForAll(Block.isNotPartOfSnake)) return false;
            return slot.Block.blockType == GameManager.instance.GameModeManager.GameMode.TypeBank.specialType;
        }
        return false;
    }
    protected override void MoveAction(Block block, BlockSlot slot, PlayerManager player = null)
    {
        slot.Block.Break(player);
        block.RawMoveTo(slot);
        if(DoToSnakeOnGrab) DoToSnakeOnGrab.Invoke(block);
        if (DoToGridOnGrab) DoToGridOnGrab.Invoke(slot.playGrid);
    }
}
