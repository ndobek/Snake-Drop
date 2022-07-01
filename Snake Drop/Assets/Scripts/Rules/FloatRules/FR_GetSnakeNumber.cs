using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Get Snake Number")]
public class FR_GetSnakeNumber : FloatRule
{

    protected override float Action(PlayerManager player)
    {
        return player.Score.NumberOfSnakes;
    }
}
