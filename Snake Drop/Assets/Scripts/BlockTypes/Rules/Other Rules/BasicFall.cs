using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/Rules/BasicFall")]
public class BasicFall : Rule
{
    public MoveRule BasicMoveRule;

    protected override void Action(Block block)
    {
        BlockSlot destination = block.Neighbor(GameManager.Direction.DOWN);
        while (BasicMoveRule.CanMoveTo(block, destination) && destination.playGrid == block.Slot.playGrid)
        {
            BasicMoveRule.OnMove(block, destination);
            destination = block.Neighbor(GameManager.Direction.DOWN);
        }
    }
}
