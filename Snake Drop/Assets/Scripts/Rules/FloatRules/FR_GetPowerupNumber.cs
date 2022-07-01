using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Get Used Powerups")]
public class FR_GetPowerupNumber : FloatRule
{
    protected override float Action(PlayerManager player)
    {
        return player.Powerup.numOfPowerupsObtained;
    }
}
