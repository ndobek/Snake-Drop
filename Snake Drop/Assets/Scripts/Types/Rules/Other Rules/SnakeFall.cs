using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/SnakeFall")]
public class SnakeFall : Rule
{
    public MoveRule moveType;

    protected override void Action(Block block)
    {
        int fallDistance = SnakeMaxFallDistance(block);

        Fall(block, fallDistance);
    }

    private void Fall(Block block, int distance)
    {
        Block tempTail = block.Tail;
        if (tempTail != null)
        {
            block.SetTail(null);
            Fall(tempTail, distance);
        }
        Debug.Log(distance);
        BlockSlot destination = block.Slot.GetNeighbor(GameManager.Direction.DOWN, distance);
        block.RawMoveTo(destination);
        //moveType.OnMove(block, destination);
        block.SetTail(tempTail);

    }

    private int BlockMaxFallDistance(Block block)
    {
        int i = 0;
        BlockSlot destination;
        while (true)
        {
            i += 1;
            destination = block.Slot.GetNeighbor(GameManager.Direction.DOWN, i);

            if (!moveType.CanMoveTo(block, destination) && !(destination && destination.Block && destination.Block.isPartOfSnake())) break;

        }
        return i -1;
    }

    private int SnakeMaxFallDistance(Block block)
    {
        if (block.Tail != null)
        {
            if (block.Tail.Slot.playGrid != block.Slot.playGrid) return 0;

            return Mathf.Min(BlockMaxFallDistance(block), SnakeMaxFallDistance(block.Tail));
        }
        else
        {
            return BlockMaxFallDistance(block);
        }
    }
}
