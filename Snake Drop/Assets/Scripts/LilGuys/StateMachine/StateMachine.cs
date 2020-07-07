using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{

    public IState currentState;

    public void setState(LilGuyState newState)
    {
        currentState = newState;
        StartCoroutine(currentState.Think(this));
    }

}
