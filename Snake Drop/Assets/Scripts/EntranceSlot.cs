using System;
using System.Collections.Generic;
using UnityEngine;

public class EntranceSlot : BlockSlot
{
    public bool Active;
    public List<PlayerManager> AllowedPlayers;

    public EntranceSlot GetNextValidSlot(GameManager.Direction direction, PlayerManager player)
    {
        GameManager.Direction nextDirection = direction;
        EntranceSlot previous = this;
        EntranceSlot result = (EntranceSlot)previous.GetNeighbor(nextDirection);
        EntranceSlot next;

        while (result && result != this && !result.CheckIfEntranceValid(player))
        {
            next = (EntranceSlot)result.GetNeighbor(nextDirection);
            if (!next)
            {
                nextDirection = GetNextDirection(nextDirection, previous);
            }
            else
            {
                previous = result;
                result = next;
            }
        }
        return result;
    }

    public GameManager.Direction GetNextDirection(GameManager.Direction PreviousDirection, EntranceSlot previousSlot)
    {
        GameManager.Direction result = GameManager.Direction.DOWN;

        int TopY = playGrid.YSize - 1;
        int BottomY = 0;

        int RightX = playGrid.XSize - 1;
        int LeftX = 0;

        if (previousSlot.y == TopY)
        {
            result = GameManager.Direction.DOWN;
        }
        if (previousSlot.y == BottomY)
        {
            result = GameManager.Direction.UP;
        }
        if (previousSlot.x == RightX)
        {
            result = GameManager.Direction.LEFT;
        }
        if (previousSlot.x == LeftX)
        {
            result = GameManager.Direction.RIGHT;
        }

        return result;

    }

    public bool CheckIfEntranceValid(PlayerManager player)
    {
        return Active == true && AllowedPlayers.Contains(player);
    }
    public bool CheckIfEntranceHasOpeningToGrid(PlayGrid grid)
    {
        bool result = false;
        if (customLeftNeighbor && customLeftNeighbor.playGrid == grid && customLeftNeighbor.Block == null) result = true;
        if (customRightNeighbor && customRightNeighbor.playGrid == grid && customRightNeighbor.Block == null) result = true;
        if (customUpNeighbor && customUpNeighbor.playGrid == grid && customUpNeighbor.Block == null) result = true;
        if (customDownNeighbor && customDownNeighbor.playGrid == grid && customDownNeighbor.Block == null) result = true;
        return result;
    }
    public BlockSlot getLinkedSlot(PlayGrid grid)
    {
        BlockSlot result = null;
        if (customLeftNeighbor && customLeftNeighbor.playGrid == grid) result = customLeftNeighbor;
        if (customRightNeighbor && customRightNeighbor.playGrid == grid) result = customRightNeighbor;
        if (customUpNeighbor && customUpNeighbor.playGrid == grid) result = customUpNeighbor;
        if (customDownNeighbor && customDownNeighbor.playGrid == grid) result = customDownNeighbor;
        return result;
    }
}
