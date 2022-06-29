using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FloatRule : ScriptableObject
{
    public float Invoke(float input, PlayerManager player = null)
    {
        return Action(input, player);
    }

    protected abstract float Action(float input, PlayerManager player = null);
}
