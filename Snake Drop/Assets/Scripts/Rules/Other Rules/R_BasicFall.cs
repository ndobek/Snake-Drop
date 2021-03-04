using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Fall Rules/Basic Fall")]
public class R_BasicFall : Rule
{
    public MoveRule BasicMoveRule;
    public Directions.Direction Direction;
    public bool RelativeToCamera;

    protected override void Action(Block block, PlayerManager player = null)
    {
        BlockSlot destination = GetDestination(block, player);

        while (BasicMoveRule.CanMoveTo(block, destination, player) && destination.playGrid == block.Slot.playGrid)
        {
            BasicMoveRule.OnMove(block, destination, player);
            destination = GetDestination(block, player);
        }
    }

    private BlockSlot GetDestination(Block block, PlayerManager player)
    {
        return RelativeToCamera? block.Neighbor(Directions.TranslateDirection(Direction, player.playerController.cameraRotator.currentDirection)): block.Neighbor(Direction);
    }
}
