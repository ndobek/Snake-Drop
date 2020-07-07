using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Increase Multiplier")]
public class R_IncreaseMultiplier : Rule
{
    public int MultiplierIncrease;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player) player.Score.Multiplier += MultiplierIncrease;
    }
}
