using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridAction : ScriptableObject
{
    public void Invoke(PlayGrid grid)
    {
        Action(grid);
    }

    protected abstract void Action(PlayGrid grid);
}
