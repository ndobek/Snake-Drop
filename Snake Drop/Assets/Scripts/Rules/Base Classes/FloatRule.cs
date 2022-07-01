using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FloatRule : ScriptableObject
{
    public float Invoke(PlayerManager player = null)
    {
        return Action(player);
    }

    protected abstract float Action(PlayerManager player = null);
}
