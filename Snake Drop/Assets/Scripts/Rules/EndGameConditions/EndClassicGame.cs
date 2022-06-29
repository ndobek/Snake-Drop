using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Rules/EndGameCondition/Classic")]
public class EndClassicGame : BoolRule
{
    public override bool Invoke(PlayerManager player)
    {
        return (!player.entranceManager.CheckForValidEntrancesToGrid(player, player.playGrid) && player.Powerup.currentPowerup == null);
    }
}
