using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilGuyThinkerBox : StateMachine, ILilGuyThinkerBox
{
    [HideInInspector]
    public LilGuy guy;

    public LilGuyState defaultState;

    private void Awake()
    {
        guy = GetComponent<LilGuy>();
        setState(defaultState);
    }

}
