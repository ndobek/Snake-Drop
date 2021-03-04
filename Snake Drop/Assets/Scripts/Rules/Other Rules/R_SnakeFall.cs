using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Fall Rules/Snake Fall")]
public class R_SnakeFall : Rule
{
    public MoveRule moveType;
    public Directions.Direction Direction;
    public bool RelativeToCamera;

    protected override void Action(Block block, PlayerManager player = null)
    {
        int fallDistance = SnakeMaxFallDistance(block, player);

        Fall(block, fallDistance, player);
    }

    private void Fall(Block block, int distance, PlayerManager player)
    {
        Block tempTail = block.Tail;
        if (tempTail != null)
        {
            block.SetTail(null);
            Fall(tempTail, distance, player);
        }
        BlockSlot destination = block.Slot.GetNeighbor(GetDirection(player), distance);
        if(destination != block.Slot) block.RawMoveTo(destination);
        //moveType.OnMove(block, destination);
        block.SetTail(tempTail);

    }

    private int BlockMaxFallDistance(Block block, PlayerManager player)
    {
        int i = 0;
        BlockSlot destination;
        while (true)
        {
            i += 1;
            destination = block.Slot.GetNeighbor(GetDirection(player), i);

            if (!moveType.CanMoveTo(block, destination) && !(destination && destination.Block && destination.Block.isPartOfSnake())) break;

        }
        return i -1;
    }
        private Directions.Direction GetDirection(PlayerManager player)
    {
        return RelativeToCamera? Directions.TranslateDirection(Direction, player.playerController.cameraRotator.currentDirection): Direction;
    }

    private int SnakeMaxFallDistance(Block block, PlayerManager player)
    {
        if (block.Tail != null)
        {
            if (block.Tail.Slot.playGrid != block.Slot.playGrid) return 0;

            return Mathf.Min(BlockMaxFallDistance(block, player), SnakeMaxFallDistance(block.Tail, player));
        }
        else
        {
            return BlockMaxFallDistance(block, player);
        }
    }
}
