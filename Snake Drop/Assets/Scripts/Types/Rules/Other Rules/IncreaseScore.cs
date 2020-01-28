using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/IncreaseScore")]
public class IncreaseScore : Rule
{
    public int ScoreIncrease;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if(player) player.Score += ScoreIncrease;
    }
}
