using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/CreateBlocks")]
public class GA_CreateBlocks : GridAction
{
    public int leftX;
    public int rightX;
    public int bottomY;
    public int topY;
    public BlockType type;
    public bool MatchSnakeColor;
    public BlockColor defaultColor;
    protected override void Action(PlayGrid grid)
    {
        PlayerManager player = GameManager.instance.playerManagers[0];
        BlockColor color = MatchSnakeColor ? player.SnakeHead.blockColor : defaultColor;
        
        for(int x = leftX; x <= rightX; x++)
        {
            for(int y = bottomY; y <= topY; y++)
            {
                BlockSlot slot = grid.GetSlot(x, y);
                slot.CreateBlock(color, type);
            }
        }
        player.DoGridActions();
    }
}

