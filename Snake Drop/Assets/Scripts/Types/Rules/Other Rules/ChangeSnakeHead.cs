using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/ChangeSnakeHead")]
public class ChangeSnakeHead : Rule
{
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            if (block == player.SnakeHead)
            {
                if (block.Tail)
                {
                    player.SnakeHead = block.Tail;
                }
                else
                {
                    player.SnakeHead = null;
                    player.OnCrash();
                }
            }
        }
    }
}
