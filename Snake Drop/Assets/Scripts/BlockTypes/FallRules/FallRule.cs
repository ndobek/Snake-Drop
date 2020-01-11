using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallRule : ScriptableObject
{
    public void OnFall(Block block)
    {
        FallAction(block);
    }

    protected abstract void FallAction(Block block);
}