using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakRule : ScriptableObject
{
    public void OnBreak(Block block)
    {
        BreakAction(block);
    }

    protected abstract void BreakAction(Block block);
}
