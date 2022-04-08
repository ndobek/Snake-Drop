using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimOnParticleCollision : MonoBehaviour
{
    public Animator anim;
    public string stateName;
    private void OnParticleCollision(GameObject other)
    {
        anim.Play(stateName);
    }
}
