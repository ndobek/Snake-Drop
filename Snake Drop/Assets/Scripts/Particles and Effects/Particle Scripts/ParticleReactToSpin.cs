using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleReactToSpin : MonoBehaviour, IReact
{
    private ParticleSystem pSystem;
    private List<ParticleSystem.Particle> newParticlesBehindPlanet = new List<ParticleSystem.Particle>();
    private List<ParticleSystem.Particle> particlesLeavingPlanet = new List<ParticleSystem.Particle>();
    private List<ParticleSystem.Particle> totalParticlesBehindPlanet = new List<ParticleSystem.Particle>();
    
    private Rigidbody rigidBody;
    public BoardRotator boardRotator;
    public float radialVMultiplier;
    public float rotationalVMultiplier;
    private void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        rigidBody = GetComponent<Rigidbody>();
        boardRotator.reactToSpin.Add(this);
    }
    private void OnParticleTrigger()
    {
        totalParticlesBehindPlanet.Clear();
        pSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, totalParticlesBehindPlanet);
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



    }
    public void React()
    {
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
        int count = pSystem.particleCount;
        ParticleSystem.Particle[] p = new ParticleSystem.Particle[count];
        pSystem.GetParticles(p);
        for (int i = 0; i < count; i++)
        {
            if (totalParticlesBehindPlanet.Contains(p[i]))
            {
                Vector3 radialV = (p[i].position - boardRotator.transform.position).normalized * radialVMultiplier;
                p[i].velocity = (rigidBody.GetPointVelocity(p[i].position) * rotationalVMultiplier) + radialV;
            }
        }
        //this one works but is slow
        pSystem.SetParticles(p);
    }



}
