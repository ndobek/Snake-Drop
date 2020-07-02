using System;
using System.Collections.Generic;
using UnityEngine;

public class EntranceSlot : BlockSlot
{
    public bool Active;
    public List<PlayerManager> AllowedPlayers;
    private EntranceAnimationManager EntranceAnimationManager;

    void Awake()
    {
        EntranceAnimationManager = GetComponent<EntranceAnimationManager>();
    }

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
                nextDirection = GetNextDirection(previous);
            }
            else
            {
                previous = result;
                result = next;
            }
        }
        return result;
    }

    public GameManager.Direction GetEdge(EntranceSlot slot)
    {
        GameManager.Direction result = GameManager.Direction.DOWN;

        int TopY = playGrid.YSize - 1;
        int BottomY = 0;

        int RightX = playGrid.XSize - 1;
        int LeftX = 0;

        if (slot.y == TopY)
        {
            result = GameManager.Direction.UP;
        }
        if (slot.y == BottomY)
        {
            result = GameManager.Direction.DOWN;
        }
        if (slot.x == RightX)
        {
            result = GameManager.Direction.RIGHT;
        }
        if (slot.x == LeftX)
        {
            result = GameManager.Direction.LEFT;
        }

        return result;
    }

    public GameManager.Direction GetNextDirection(EntranceSlot slot)
    {
        return GameManager.GetOppositeDirection(GetEdge(slot));
    }

    public bool CheckIfEntranceValid(PlayerManager player)
    {
        return Active == true && AllowedPlayers.Contains(player);
    }
    public bool CheckIfEntranceHasOpeningToGrid(PlayGrid grid)
    {
        BlockSlot slot = getOpeningToGrid(grid);
        return (slot && slot.Block == null);
    }
    public BlockSlot getOpeningToGrid(PlayGrid grid)
    {
        BlockSlot result = null;
        if (customLeftNeighbor && customLeftNeighbor.playGrid == grid) result = customLeftNeighbor;
        if (customRightNeighbor && customRightNeighbor.playGrid == grid) result = customRightNeighbor;
        if (customUpNeighbor && customUpNeighbor.playGrid == grid) result = customUpNeighbor;
        if (customDownNeighbor && customDownNeighbor.playGrid == grid) result = customDownNeighbor;
        return result;
    }

    public void UpdateAnimations()
    {
        /*if(EntranceAnimationManager !=null)*/ EntranceAnimationManager.UpdateSprite();
    }
}
