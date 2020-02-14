using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Fall Rules/Basic Fall")]
public class R_BasicFall : Rule
{
    public MoveRule MoveRule;

    protected override void Action(Block block, PlayerManager player = null)
    {
        BlockSlot destination = block.Neighbor(GameManager.Direction.DOWN);
        while (MoveRule.CanMoveTo(block, destination, player) && destination.playGrid == block.Slot.playGrid)
        {
            MoveRule.OnMove(block, destination, player);
            destination = block.Neighbor(GameManager.Direction.DOWN);
        }
    }
}
