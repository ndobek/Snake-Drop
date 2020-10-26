using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/EndGameCondition/EndGame")]
public class EndGame : EndGameCondition
{
    public override bool GameIsOver(PlayerManager player)
    {
        return true;
    }
}
