using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem xPParticleSystem;
    public ParticleSystem blockBreakParticleSystem;
    public int numberOfBlockBreakParticles = 8;
    
    public void TriggerXPParticles(int xP, Vector3 pos)
    {        
        xPParticleSystem.gameObject.transform.position = pos;
        xPParticleSystem.Emit(xP + 1);        
    }

    public void TriggerBlockBreakParticles(string color, Vector3 pos)
    {
        blockBreakParticleSystem.gameObject.transform.position = pos;
        blockBreakParticleSystem.Emit(numberOfBlockBreakParticles);



       
    }
}
