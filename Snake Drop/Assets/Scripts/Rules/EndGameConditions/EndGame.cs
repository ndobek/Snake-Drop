using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/EndGameCondition/EndGame")]
public class EndGame : BoolRule
{
    public override bool Invoke(PlayerManager player)
    {
        return true;
    }
}
