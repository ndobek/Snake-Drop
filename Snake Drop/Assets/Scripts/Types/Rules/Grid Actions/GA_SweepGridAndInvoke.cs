using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/Sweep Grid To Invoke")]
public class GA_SweepGridAndInvoke : GridAction
{
    public Rule RuleToInvoke;
    public GameManager.Direction Direction;
    protected override void Action(PlayGrid grid)
    {
        grid.dirty = true;

        while (grid.dirty)
        {
            grid.dirty = false;
            switch (Direction)
            {
                case GameManager.Direction.UP:
                    for (int x = 0; x < grid.XSize; x++)
                    {
                        for (int y = 0; y < grid.YSize; y++)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;

                case GameManager.Direction.DOWN:
                    for (int x = grid.XSize - 1; x >= 0; x--)
                    {
                        for (int y = grid.YSize - 1; y >= 0 ; y--)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;

                case GameManager.Direction.LEFT:
                    for (int y = grid.YSize - 1; y >= 0; y--)
                    {
                        for (int x = grid.XSize - 1; x >= 0; x--)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;

                case GameManager.Direction.RIGHT:
                    for (int y = 0; y < grid.YSize; y++)
                    {
                        for (int x = 0; x < grid.XSize; x++)
                        {
                            InvokeOnBlock(grid.GetBlock(x, y), grid);
                        }
                    }
                    break;
            }


            grid.Update();
        }
    }
    public void InvokeOnBlock(Block block, PlayGrid grid)
    {
        if (block && block.Slot.playGrid == grid)
        {
            if (RuleToInvoke != null)
            {
                RuleToInvoke.Invoke(block);
            }
            else
            {
                block.OnGridAction(block.Owner);
            }
        }
    }
}
