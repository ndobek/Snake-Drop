using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarCycler : MonoBehaviour, ICycleThings
{
    public ICyclical sky;
    
    void Start()
    {
        sky = GetComponentInChildren<ICyclical>();
    }
    public void CycleThings()
    {
        sky.CycleUpdate();
    }

    private void Update()
    {
        CycleThings();
    }

}
