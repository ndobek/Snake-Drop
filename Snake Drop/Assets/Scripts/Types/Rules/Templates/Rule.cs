using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : ScriptableObject
{
    public void Invoke(Block block)
    {
        Action(block);
    }

    protected abstract void Action(Block block);
}