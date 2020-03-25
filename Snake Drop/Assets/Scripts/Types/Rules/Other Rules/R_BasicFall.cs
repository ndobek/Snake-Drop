using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Fall Rules/Basic Fall")]
public class R_BasicFall : Rule
{
    public MoveRule BasicMoveRule;
    public GameManager.Direction Direction;

    protected override void Action(Block block, PlayerManager player = null)
    {
        BlockSlot destination = block.Neighbor(Direction);
        while (BasicMoveRule.CanMoveTo(block, destination, player) && destination.playGrid == block.Slot.playGrid)
        {
            BasicMoveRule.OnMove(block, destination, player);
            destination = block.Neighbor(Direction);
        }
    }
}
