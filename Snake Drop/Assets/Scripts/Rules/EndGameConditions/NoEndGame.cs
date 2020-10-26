using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/EndGameCondition/NoEndGame")]
public class NoEndGame : EndGameCondition
{
    public override bool GameIsOver(PlayerManager player)
    {
        return false;
    }
}
