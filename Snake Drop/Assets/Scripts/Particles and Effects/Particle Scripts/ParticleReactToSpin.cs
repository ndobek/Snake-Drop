using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleReactToSpin : MonoBehaviour, IReact
{
    public List<ParticleSystem> pSystems;
    // private List<ParticleSystem.Particle> totalParticlesBehindPlanet = new List<ParticleSystem.Particle>();
    public float maxParticlesPerSpin;
    public float minParticlesPerSpin;
    // private Rigidbody rigidBody;
    public BoardRotator boardRotator;
    public int maxVariation;
    //public float radialVMultiplier;
   // public float rotationalVMultiplier;
    private void Start()
    {
        //rigidBody = GetComponent<Rigidbody>();
        boardRotator.reactToSpin.Add(this);
    }
   // private void OnParticleTrigger()
   // {
       // totalParticlesBehindPlanet.Clear();
        //pSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, totalParticlesBehindPlanet);
        //pSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, newParticlesBehindPlanet);
        //if (newParticlesBehindPlanet.Count > 0)
        //{
        //    for (int i = 0; i < newParticlesBehindPlanet.Count; i++)
        //    {
                
        //        totalParticlesBehindPlanet.Add(newParticlesBehindPlanet[i]);
        //    }
            
        //}
        //pSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, particlesLeavingPlanet);
        //if (particlesLeavingPlanet.Count > 0)
        //{
        //    for(int i = 0; i < particlesLeavingPlanet.Count; i++)
        //    {
                
        //        totalParticlesBehindPlanet.Remove(particlesLeavingPlanet[i]);
        //    }
        //}



    //}
    public void React()
    {
        int emissionCount = (int)UnityEngine.Random.Range(minParticlesPerSpin, maxParticlesPerSpin);
        if (pSystems != null && pSystems.Count > 0)
        {
            foreach (ParticleSystem pSystem in pSystems)
            {
                int variation = (int)UnityEngine.Random.Range(0, maxVariation);
                pSystem.Emit(emissionCount + variation);
                
            }
        }
            
    #region bad
    //for (int i = 0; i < totalParticlesBehindPlanet.Count; i++)
    //{
    //    ParticleSystem.Particle particle = totalParticlesBehindPlanet[i];
    //    Vector3 radialV = (particle.position - boardRotator.transform.position).normalized * radialVMultiplier;

    //    Vector3 rotationalV = (rigidBody.GetPointVelocity(particle.position) * rotationalVMultiplier) ;
    //    particle.velocity = rotationalV + radialV;
    //    totalParticlesBehindPlanet[i] = particle;
    //}
    //pSystem.SetParticles(totalParticlesBehindPlanet.ToArray());
    ////don't cursed code uncomment inside
    ///
    #endregion
    #region ehh
    //int count = pSystem.particleCount;
    //ParticleSystem.Particle[] p = new ParticleSystem.Particle[count];
    //pSystem.GetParticles(p);
    //for (int i = 0; i < totalParticlesBehindPlanet.Count; i++)
    //{

    //    int pIndex = Array.IndexOf(p, totalParticlesBehindPlanet[i]);
    //    if(pIndex >= 0)
    //    {

    //        Vector3 radialV = (p[pIndex].position - boardRotator.transform.position).normalized * radialVMultiplier;
    //        p[pIndex].velocity = (rigidBody.GetPointVelocity(p[pIndex].position) * rotationalVMultiplier + radialV);

    //    }
    //}
    #endregion
    #region the best so far I guess
    //int count = pSystem.particleCount;
    //ParticleSystem.Particle[] p = new ParticleSystem.Particle[count];
    //pSystem.GetParticles(p);
    //for (int i = 0; i < count; i++)
    //{
    //    if (totalParticlesBehindPlanet.Contains(p[i]))
    //    {
    //        Vector3 radialV = (p[i].position - boardRotator.transform.position).normalized * radialVMultiplier;
    //       // p[i].velocity = (rigidBody.GetPointVelocity(p[i].position) * rotationalVMultiplier) + radialV;
    //    }
    //}
    ////this one works but is slow
    //pSystem.SetParticles(p);
}
#endregion

    

}
