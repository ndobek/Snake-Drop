using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    public static Direction TranslateDirection(Direction directionToTranslate, Direction newUpDirection)
    {
        switch (newUpDirection)
        {
            case Direction.UP: return directionToTranslate;
            case Direction.DOWN: return GetOppositeDirection(directionToTranslate);
            case Direction.LEFT: return GetCounterClockwiseNeighborDirection(directionToTranslate);
            case Direction.RIGHT: return GetClockwiseNeighborDirection(directionToTranslate);
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }

    public static Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.DOWN;
            case Direction.DOWN: return Direction.UP;
            case Direction.LEFT: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.LEFT;
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }
    public static Direction GetClockwiseNeighborDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.RIGHT;
            case Direction.DOWN: return Direction.LEFT;
            case Direction.LEFT: return Direction.UP;
            case Direction.RIGHT: return Direction.DOWN;
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }
    public static Direction GetCounterClockwiseNeighborDirection(Direction direction)
    {
        return GetOppositeDirection(GetClockwiseNeighborDirection(direction));
    }
    public static Directions.Direction TailDirection(Block block)
    {
        return DirectionTo(block, block.Tail);
    }
    public static Directions.Direction DirectionTo(Block blockFrom, Block blockTo)
    {
        return DirectionTo(blockFrom.Slot, blockTo.Slot);
    }
    public static Directions.Direction DirectionTo(BlockSlot blockSlotFrom, BlockSlot blockSlotTo)
    {

        Vector2 from = blockSlotFrom.Coords;
        Vector2 to = blockSlotTo.Coords;
        Vector2 dif = to - from;
        if (Math.Abs(dif.x)> Math.Abs(dif.y))
        {
            if(dif.x >= 0)
            {
                return Direction.RIGHT;
            }
            else
            {
                return Direction.LEFT;
            }
        }
        else
        {
            if (dif.y >= 0)
            {
                return Direction.UP;
            }
            else
            {
                return Direction.DOWN;
            }
        }
        //foreach (Directions.Direction dir in Enum.GetValues(typeof(Directions.Direction)))
        //{

        //    if (blockFrom.Neighbor(dir) == blockTo.Slot)
        //    {
        //        return dir;
        //    }
        //}
        //Debug.LogError("Ye booched it!");
        //return default;

    }
    public static Directions.Direction HeadDirection(Block block) //Alyssa Maked This
    {
        return DirectionTo(block, block.Head);
    }
}

