using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotator : MonoBehaviour, ICyclical
{
    public GameObject sky;
    [SerializeField]
    private float cycleLength;//seconds
    public float CycleLength 
    { 
        get 
        {
            return cycleLength;
        } 
    }

    private float cyclePoint = 0;
    public float CyclePoint 
    {
        get { return cyclePoint; }
    }
    
    public void CycleUpdate()
    {
        float updatesPerCycle = (cycleLength) / Time.deltaTime;
        Vector3 rotationAmount = new Vector3(0, 0, (360 / updatesPerCycle));
        sky.transform.Rotate(rotationAmount);
        if (cyclePoint >= 1) { cyclePoint = 0; }
        cyclePoint += rotationAmount.z;

    }
}
