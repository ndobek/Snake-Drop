﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<GameObject> xPPSystemObjPool = new List<GameObject>();
    public List<GameObject> blockBreakPSystemObjPool = new List<GameObject>();
    public List<ParticleSystem> xPPSystemPool = new List<ParticleSystem>();
    public List<ParticleSystem> blockBreakPSystemPool = new List<ParticleSystem>();
    

    public GameObject initialXPPSystem;
    public GameObject initialBlockBreakPSystem;
    public int numberOfBlockBreakParticles = 8;
    [SerializeField]
    private int xPIndex = 0;
    
    public int xPPoolSize = 5;
    public int blockBreakPoolSize = 15;
    [SerializeField]
    private int blockBreakIndex = 0;
    public float pSystemZCoord;

    public void InitializePSystems()
    {
        int i = 0;
        while (i < xPPoolSize)
        {
            GameObject babyParticleSystemObj = Instantiate<GameObject>(initialXPPSystem);
            ParticleSystem babyParticleSystem = babyParticleSystemObj.GetComponent<ParticleSystem>();
            xPPSystemObjPool.Add(babyParticleSystemObj);
            xPPSystemPool.Add(babyParticleSystem);
            i++;
        }
        i = 0;
        while (i < blockBreakPoolSize)
        {
            GameObject babyParticleSystemObj = Instantiate<GameObject>(initialBlockBreakPSystem);
            ParticleSystem babyParticleSystem = babyParticleSystemObj.GetComponent<ParticleSystem>();
            blockBreakPSystemObjPool.Add(babyParticleSystemObj);
            blockBreakPSystemPool.Add(babyParticleSystem);
            i++;
        }

    }
    public ParticleSystem RetrieveParticleSystem(List<ParticleSystem> list, ref int index)
    {

        ParticleSystem result = list[index];
        index++;
        if (index >= (list.Count - 1))
        {
            index = 0;
        }
        return result;
    }
    public ParticleSystem PeekNextXPSystem()
    {
        int i = xPIndex;
        if(i >= (xPPSystemPool.Count - 1))
        {
            i = 0;
        }
        return xPPSystemPool[i];
    }
    public void TriggerXPParticles(int xP, Vector3 pos, Color color)
    {
        ParticleSystem pSystem = RetrieveParticleSystem(xPPSystemPool, ref xPIndex);
        ParticleSystem.MainModule main = pSystem.main;
        //pos.z = pSystemZCoord;
        pSystem.transform.position = pos;
        main.startColor = color;
        pSystem.Emit(xP + 1);        
    }

    public void TriggerBlockBreakParticles(Color color, Vector3 pos)
    {
        ParticleSystem pSystem = RetrieveParticleSystem(blockBreakPSystemPool, ref blockBreakIndex);
        ParticleSystem.MainModule main = pSystem.main;
        //pos.z = pSystemZCoord;
        pSystem.transform.position = pos;
        main.startColor = color;
        pSystem.Emit(numberOfBlockBreakParticles);
    }
    private void Start()
    {
        InitializePSystems();
    }
}
