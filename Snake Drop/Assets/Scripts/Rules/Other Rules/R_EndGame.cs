using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Rules/General/End Game")]
public class R_EndGame : Rule
{
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player) player.EndGame();
    }
}
