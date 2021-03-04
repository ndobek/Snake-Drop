using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EndGameCondition : ScriptableObject
{
    public abstract bool GameIsOver(PlayerManager player);
}
