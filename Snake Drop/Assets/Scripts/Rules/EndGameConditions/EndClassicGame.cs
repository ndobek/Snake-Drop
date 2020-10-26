using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Rules/EndGameCondition/Classic")]
public class EndClassicGame : EndGameCondition
{
    public override bool GameIsOver(PlayerManager player)
    {
        return !player.entranceManager.CheckForValidEntrancesToGrid(player, player.playGrid);
    }
}
