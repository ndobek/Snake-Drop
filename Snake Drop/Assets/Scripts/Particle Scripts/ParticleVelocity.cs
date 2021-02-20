using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleVelocity : MonoBehaviour
{
    public ParticleSystem pSystem;
    public BoxCollider2D progressGoo;
    public Vector2 particleHeading;
    private void Zip(Transform target)
    {
        particleHeading = target.position;

        var velocity = pSystem.velocityOverLifetime;
        var shape = pSystem.shape;
        shape.enabled = true;
        velocity.enabled = true;
        shape.sphericalDirectionAmount = 0f;
        velocity.orbitalZ = 0f;
        velocity.x = particleHeading.x;
        velocity.y = particleHeading.y;
    }
    private void Start()
    {
        Zip(progressGoo.transform);
    }

}
