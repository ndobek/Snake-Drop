using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarCollider : MonoBehaviour
{
    BoxCollider target;
    GameObject xPParticleSystemObj;
    ParticleSystem xPParticleSystem;
    List<ParticleCollisionEvent> hits = new List<ParticleCollisionEvent>();
    private void Start()
    {
        target = GetComponent<BoxCollider>();
        xPParticleSystem = xPParticleSystemObj.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject pSystem)
    {
        if (pSystem.tag == "XPParticleSystem")
        {
            int hitsCount = ParticlePhysicsExtensions.GetCollisionEvents(xPParticleSystem, pSystem, hits);
                
            for (int i = 0; i<hitsCount; i++)
            {
                //hits[i].
            }
                  
        }
        
    }
}
