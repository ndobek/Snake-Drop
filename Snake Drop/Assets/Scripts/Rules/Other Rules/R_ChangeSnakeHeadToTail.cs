using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Change Snake Head To Tail")]
public class R_ChangeSnakeHeadToTail : Rule
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
                    player.OnCrash(false);
                }
            }
        }
    }
}
