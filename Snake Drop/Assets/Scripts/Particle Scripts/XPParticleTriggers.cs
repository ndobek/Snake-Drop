using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class XPParticleTriggers : MonoBehaviour
{
    public GameObject pControllerObj;
    public ITarget pController;
    

    private ParticleSystem pSystem;
    public GameObject target;
    private List<ParticleCollisionEvent> hits = new List<ParticleCollisionEvent>();
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private void Start()
    {
        pController = pControllerObj.GetComponent<ITarget>();
        pSystem = GetComponent<ParticleSystem>();
        InitializeParticles();
       
    }
    private void InitializeParticles()
    {
        var triggers = pSystem.trigger;
        triggers.enabled = true;

    }
    private void OnParticleCollision(GameObject trigger)
    {
        {
            
            if (trigger.tag == "MagnetFieldTrigger")
            {
                pController.MagnetActivate(pSystem, target.transform);
                Debug.Log("eek");
            }
                
            if (trigger.tag == "ProgressBarTrigger")
            {
                ParticlePhysicsExtensions.GetTriggerParticles(pSystem, ParticleSystemTriggerEventType.Enter, particles);
                List<ParticleSystem.Particle> newParticles = new List<ParticleSystem.Particle>();
                foreach (ParticleSystem.Particle p in particles)
                {
                    ParticleSystem.Particle newParticle = p;
                    newParticle.remainingLifetime = 0;
                    newParticles.Add(newParticle);
                }
                ParticlePhysicsExtensions.SetTriggerParticles(pSystem, ParticleSystemTriggerEventType.Enter, newParticles);
                Debug.Log("ow");
            }           
        }  
    }
}

