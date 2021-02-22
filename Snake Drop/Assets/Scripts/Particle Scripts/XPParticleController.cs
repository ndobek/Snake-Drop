using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPParticleController : MonoBehaviour, ITarget
{
    public GameObject targetTest;
    public ParticleSystem pSystemTest;
    public void LooseTarget(ParticleSystem pSystem, Transform target)
    {
        Vector3 particleHeading = target.position;

        

        var velocity = pSystem.velocityOverLifetime;
        var shape = pSystem.shape;
        shape.enabled = true;
        velocity.enabled = true;
        shape.sphericalDirectionAmount = 0f;
        velocity.orbitalZ = 0f;
        velocity.x = particleHeading.x;
        velocity.y = particleHeading.y;
    }
    public void MagnetActivate(ParticleSystem pSystem, Transform target)
    {
        var velocity = pSystem.velocityOverLifetime;
        var shape = pSystem.shape;
        var noise = pSystem.noise;
        noise.enabled = true;
        shape.enabled = true;
        velocity.enabled = true;
        noise.strength = 0;
        Vector3 particleHeading = target.position;
        velocity.x = particleHeading.x;
        velocity.y = particleHeading.y;
        shape.randomDirectionAmount = 0;
        
    }
    private void Start()
    {
        LooseTarget(pSystemTest, targetTest.transform);
    }


}
