using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallRule : ScriptableObject
{
    public void OnFall(Block block, BlockSlot slot)
    {
        FallAction(block, slot);
    }

    protected abstract void FallAction(Block block, BlockSlot slot);
}