using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/MoveRule/Basic")]
public class BasicMove : MoveRule
{
    protected override bool MoveCondition(Block block, BlockSlot slot, PlayerManager player = null)
    {
        return slot && slot.Block == null;
    }
    protected override void MoveAction(Block block, BlockSlot slot, PlayerManager player = null)
    {
        block.RawMoveTo(slot, Animation);
    }
}
