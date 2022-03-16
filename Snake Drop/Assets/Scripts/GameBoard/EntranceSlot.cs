using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class EntranceSlot : BlockSlot
{
    public bool ActiveByDefault;
    public bool SelectableByDefault;
    [HideInInspector]
    public bool Active;
    [HideInInspector]
    public bool Selectable = true;
    public List<PlayerManager> AllowedPlayers;
    private IEntranceAnimationBehavior EntranceAnimationManager;

    void Awake()
    {
        EntranceAnimationManager = GetComponent<IEntranceAnimationBehavior>();
        ReActivate();
    }

    public EntranceSlot GetNextValidSlot(Directions.Direction direction, PlayerManager player)
    {
        Directions.Direction nextDirection = direction;
        EntranceSlot previous = this;
        EntranceSlot result = this;
        EntranceSlot next;
        bool runMinOnce = false;

        while (runMinOnce == false || (result && result != this && result.Selectable == false)/*&& !result.CheckIfEntranceValid(player)*/)
        {
            runMinOnce = true;
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
    public EntranceSlot GetNextValidSlot(bool clockwise, PlayerManager player)
    {
        Directions.Direction nextDirection = clockwise ? Directions.GetClockwiseNeighborDirection(GetEdgeInfo().direction()) : Directions.GetCounterClockwiseNeighborDirection(GetEdgeInfo().direction());
        EntranceSlot result = this;
        object next;
        bool runMinOnce = false;

        while (runMinOnce == false || (result && result != this && result.Selectable == false))
        {
            runMinOnce = true;
            next = result.GetNeighbor(nextDirection);
            if (next == null || !(next is EntranceSlot))
            {
                nextDirection = clockwise? Directions.GetClockwiseNeighborDirection(nextDirection) : Directions.GetCounterClockwiseNeighborDirection(nextDirection);
            }
            else
            {
                result = (EntranceSlot)next;
            }
        }
        return result;
    }





    public Directions.Direction GetNextDirection(EntranceSlot slot)
    {
        return Directions.GetOppositeDirection(slot.GetEdgeInfo().direction()); ;
    }

    public bool CheckIfEntranceValid(PlayerManager player = null, PlayGrid playGrid = null)
    {
                
        return Active == true 
            && (player == null || AllowedPlayers.Contains(player))
            && CheckIfEntranceHasOpeningToGrid(playGrid)
            && player.GetNextSnakeHead().CanMoveToWithoutCrashing(GetOpeningToGrid(playGrid));
    }
    public bool CheckIfEntranceHasOpeningToGrid(PlayGrid grid)
    {
        BlockSlot slot = GetOpeningToGrid(grid);
        return (slot/* && slot.Block == null*/);
    }
    public BlockSlot GetOpeningToGrid(PlayGrid grid)
    {
        return GetNeighbor(GetEntranceDirection(grid));
        
    }
    public Directions.Direction GetEntranceDirection(PlayGrid grid)
    {
        Directions.Direction result = Directions.Direction.DOWN;
        if (customLeftNeighbor && customLeftNeighbor.playGrid == grid) result = Directions.Direction.LEFT;
        if (customRightNeighbor && customRightNeighbor.playGrid == grid) result = Directions.Direction.RIGHT;
        if (customUpNeighbor && customUpNeighbor.playGrid == grid) result = Directions.Direction.UP;
        if (customDownNeighbor && customDownNeighbor.playGrid == grid) result = Directions.Direction.DOWN;
        return result;

    }

    public void UpdateAnimations()
    {
        if (EntranceAnimationManager != null) EntranceAnimationManager.UpdateSprite();
    }

    public void ReActivate()
    {
        Active = ActiveByDefault;
        Selectable = SelectableByDefault;
    }
}
