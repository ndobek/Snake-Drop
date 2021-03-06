using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<GameObject> xPPSystemPool = new List<GameObject>();
    public List<GameObject> blockBreakPSystemPool = new List<GameObject>();
    public GameObject initialXPPSystem;
    public GameObject initialBlockBreakPSystem;
    public int numberOfBlockBreakParticles = 8;
    private int xPIndex = 0;
    public int xPPoolSize = 5;
    public int blockBreakPoolSize = 15;
    private int blockBreakIndex = 0;
    public float pSystemZCoord;

    public void InitializePSystems()
    {
        int i = 0;
        while (i < xPPoolSize)
        {
            GameObject babyParticleSystem = Instantiate<GameObject>(initialXPPSystem);
            xPPSystemPool.Add(babyParticleSystem);
            i++;
        }
        i = 0;
        while (i < blockBreakPoolSize)
        {
            GameObject babyParticleSystem = Instantiate<GameObject>(initialBlockBreakPSystem);
            blockBreakPSystemPool.Add(babyParticleSystem);
            i++;
        }

    }
    public GameObject RetrieveParticleSystem(List<GameObject> list, int index)
    {

        GameObject result = list[index];
        index++;
        if (index >= (list.Count - 1))
        {
            index = 0;
        }
        return result;
    }
    public void TriggerXPParticles(int xP, Vector3 pos)
    {
        GameObject pSystem = RetrieveParticleSystem(xPPSystemPool, xPIndex);
        
        pos.z = pSystemZCoord;
        pSystem.transform.position = pos;
        pSystem.GetComponent<ParticleSystem>().Emit(xP + 1);        
    }

    public void TriggerBlockBreakParticles(string color, Vector3 pos)
    {
        GameObject pSystem = RetrieveParticleSystem(blockBreakPSystemPool, blockBreakIndex);
        pos.z = pSystemZCoord;
        pSystem.transform.position = pos;
        pSystem.GetComponent<ParticleSystem>().Emit(numberOfBlockBreakParticles);
    }
    private void Start()
    {
        InitializePSystems();
    }
}
