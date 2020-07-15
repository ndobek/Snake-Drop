using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/ReactivateEntrances")]
public class GA_ReactivateEntrancesIfGridEmpty : GridAction
{
    protected override void Action(PlayGrid grid)
    {
        EntranceManager EntranceGrid = grid as EntranceManager;
        if (EntranceGrid != null && GridEmpty(EntranceGrid.LinkedTo))
        {
            EntranceGrid.ReactivateEntrances();
        }
    }
    private bool GridEmpty(PlayGrid grid)
    {
        bool result = true;
        foreach(BlockSlot slot in grid.slots)
        {
            if (slot && slot.Block && !slot.Block.isPartOfSnake()) result = false;
        }
        return result;
    }
}
