using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : ScriptableObject
{
    public void Invoke(Block block, PlayerManager player = null)
    {
        Action(block, player);
    }

    protected abstract void Action(Block block, PlayerManager player = null);
}