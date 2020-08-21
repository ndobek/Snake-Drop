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
        foreach (Directions.Direction dir in Enum.GetValues(typeof(Directions.Direction)))
        {
            if (blockFrom.Neighbor(dir) == blockTo) return dir;
        }
        return default;
    }
}

