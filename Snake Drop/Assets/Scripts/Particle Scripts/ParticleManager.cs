using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem xPParticleSystem;
    public ParticleSystem blockBreakParticleSystemBlue;
    public ParticleSystem blockBreakParticleSystemYellow;
    public ParticleSystem blockBreakParticleSystemGreen;
    public ParticleSystem blockBreakParticleSystemMagenta;
    public ParticleSystem blockBreakParticleSystemOrange;
    public int numberOfBlockBreakParticles = 8;
    
    public void TriggerXPParticles(int xP, Vector3 pos)
    {        
        xPParticleSystem.gameObject.transform.position = pos;
        xPParticleSystem.Emit(xP + 1);        
    }

    public void TriggerBlockBreakParticles(string color, Vector3 pos)
    {
        switch (color)
        {
            case "Blue":
                blockBreakParticleSystemBlue.gameObject.transform.position = pos;
                blockBreakParticleSystemBlue.Emit(numberOfBlockBreakParticles);
                break;
            case "Green":
                blockBreakParticleSystemGreen.gameObject.transform.position = pos;
                blockBreakParticleSystemGreen.Emit(numberOfBlockBreakParticles);
                break;
            case "Orange":
                blockBreakParticleSystemOrange.gameObject.transform.position = pos;
                blockBreakParticleSystemOrange.Emit(numberOfBlockBreakParticles);
                break;
            case "Yellow":
                blockBreakParticleSystemYellow.gameObject.transform.position = pos;
                blockBreakParticleSystemYellow.Emit(numberOfBlockBreakParticles);
                break;
            case "Red":
                blockBreakParticleSystemMagenta.gameObject.transform.position = pos;
                blockBreakParticleSystemMagenta.Emit(numberOfBlockBreakParticles);
                break;



        }
    }
}
