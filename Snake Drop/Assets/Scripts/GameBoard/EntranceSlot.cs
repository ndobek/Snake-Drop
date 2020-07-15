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

    public EntranceSlot GetNextValidSlot(GameManager.Direction direction, PlayerManager player)
    {
        GameManager.Direction nextDirection = direction;
        EntranceSlot previous = this;
        EntranceSlot result = (EntranceSlot)previous.GetNeighbor(nextDirection);
        EntranceSlot next;

        while (result && result != this && result.Selectable == false/*&& !result.CheckIfEntranceValid(player)*/)
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



    public GameManager.Direction GetNextDirection(EntranceSlot slot)
    {
        return GameManager.GetOppositeDirection(slot.GetEdgeInfo().direction()); ;
    }

    public bool CheckIfEntranceValid(PlayerManager player = null, PlayGrid playGrid = null)
    {
                
        return Active == true 
            && (player == null || AllowedPlayers.Contains(player))
            && CheckIfEntranceHasOpeningToGrid(playGrid)
            && player.GetNextSnakeHead().CanMoveToWithoutCrashing(getOpeningToGrid(playGrid));
    }
    public bool CheckIfEntranceHasOpeningToGrid(PlayGrid grid)
    {
        BlockSlot slot = getOpeningToGrid(grid);
        return (slot/* && slot.Block == null*/);
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
        if (EntranceAnimationManager != null) EntranceAnimationManager.UpdateSprite();
    }

    public void ReActivate()
    {
        Active = ActiveByDefault;
        Selectable = SelectableByDefault;
    }
}
