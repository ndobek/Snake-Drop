using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Increase MultiplierTwo")]
public class R_IncreaseMultiplierTwo : Rule
{
    public int MultiplierIncrease;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player.mostRecentBlockColorEaten == null) player.mostRecentBlockColorEaten = block.blockColor;
        if (player && block.blockColor != player.mostRecentBlockColorEaten)
        {
            player.Score.MultiplierTwo += MultiplierIncrease;
            player.mostRecentBlockColorEaten = block.blockColor;
        }
    }
}
