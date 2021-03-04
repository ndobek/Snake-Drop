using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/Sweep Grid To Invoke")]
public class GA_SweepGridAndInvoke : GridAction
{
    public Rule RuleToInvoke;
    public Directions.Direction Direction;
    protected override void Action(PlayGrid grid)
    {
        grid.Dirty = true;

        while (grid.Dirty)
        {
            grid.Dirty = false;
            switch (Direction)
            {
                case Directions.Direction.UP:
                    for (int x = 0; x < grid.XSize; x++)
                    {
                        for (int y = 0; y < grid.YSize; y++)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;

                case Directions.Direction.DOWN:
                    for (int x = grid.XSize - 1; x >= 0; x--)
                    {
                        for (int y = grid.YSize - 1; y >= 0 ; y--)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;

                case Directions.Direction.LEFT:
                    for (int y = grid.YSize - 1; y >= 0; y--)
                    {
                        for (int x = grid.XSize - 1; x >= 0; x--)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;

                case Directions.Direction.RIGHT:
                    for (int y = 0; y < grid.YSize; y++)
                    {
                        for (int x = 0; x < grid.XSize; x++)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;
            }
        }
    }
    public void InvokeOnBlock(Block block, PlayGrid grid)
    {
        if (block && block.Slot.playGrid == grid)
        {

            if (RuleToInvoke != null)
            {
                //RuleToInvoke.Invoke(block, block.Owner);
                RuleToInvoke.Invoke(block, GameManager.instance.playerManagers[0]);
            }
            else
            {
                //block.SpecialAction(block.Owner);
                block.SpecialAction(GameManager.instance.playerManagers[0]);
            }
        }
    }
}
