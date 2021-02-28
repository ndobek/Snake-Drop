using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeatherCycle : MonoBehaviour, ICyclical
{
    public Cycler wCycler;
    public float CycleLength => throw new System.NotImplementedException();

    public float CyclePoint => throw new System.NotImplementedException();

    public void CycleUpdate()
    {
        throw new System.NotImplementedException();
    }
    void Start()
    {
        wCycler.cyclicalBehaviours.Add(this);
    }
}
