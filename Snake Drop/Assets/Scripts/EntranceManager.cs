using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : PlayGrid
{
    public PlayGrid LinkedTo;
    public List<PlayerManager> AllowedPlayers;

    public EntranceSlot StartingSlot;

    public override void CreateGrid()
    {
        XSize = LinkedTo.XSize + 2;
        YSize = LinkedTo.YSize + 2;
        GridSpace = LinkedTo.GridSpace;
        base.CreateGrid();
    }

    protected override void CreateSlot(int x, int y)
    {
        if (x == 0 | x == XSize - 1 | y == 0 | y == YSize - 1)
        {
            base.CreateSlot(x, y);
            Vector3 rotation = Vector3.zero;
            EntranceSlot slot = (EntranceSlot)slots[FlattenedIndex(x, y)];

            //Facing Right
            if (x == 0)
            {
                rotation = new Vector3(0, 0, 90);
                slot.customRightNeighbor = LinkedTo.GetSlot(0, y - 1);
            }
            //Facing Left
            else if (x == XSize - 1)
            {
                rotation = new Vector3(0, 0, -90);
                slot.customLeftNeighbor = LinkedTo.GetSlot(LinkedTo.XSize - 1, y - 1);
            }
            //Facing Up
            else if (y == 0)
            {
                rotation = new Vector3(0, 0, 180);
                slot.customUpNeighbor = LinkedTo.GetSlot(x - 1, 0);
            }
            //Facing Down
            else if (y == YSize - 1)
            {
                rotation = new Vector3(0, 0, 0);
                slot.customDownNeighbor = LinkedTo.GetSlot(x - 1, LinkedTo.YSize - 1);
            }

            slot.Active = true;
            slot.AllowedPlayers = AllowedPlayers;
            slot.transform.rotation = Quaternion.Euler(rotation);

        }
    }

    public bool CheckForValidEntrancesToGrid(PlayerManager player, PlayGrid grid)
    {
        bool result = false;
        foreach (EntranceSlot slot in slots)
        {
            if (slot && slot.CheckIfEntranceValid(player) && slot.CheckIfEntranceHasOpeningToGrid(grid)) result = true;
        }
        return result;
    }

}
