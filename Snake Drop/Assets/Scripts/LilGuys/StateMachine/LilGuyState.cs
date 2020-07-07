using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LilGuyState : ScriptableObject, IState
{
    public LilGuy guy;

    public LilGuyState(LilGuy Guy)
    {
        guy = Guy;
    }

    public virtual IEnumerator Think(StateMachine machine)
    {
        yield return null;
    }

}
