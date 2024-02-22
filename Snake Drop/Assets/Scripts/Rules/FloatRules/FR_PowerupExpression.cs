using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/PowerupExpression")]
public class FR_PowerupExpression : FloatRule
{
    protected override float Action(PlayerManager player)
    {
        int pu = player.Powerup.numOfPowerupsObtained;

        return 100000 + (200 * pu * pu) + (1000000 / (20 + ((pu - 10) *(pu-10))));
    }
}
