using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AnimatableLight : MonoBehaviour, IAnimatableEffect
{
    public Light2D lightComponent;
    public IEffectAnimator EffectAnimator { get; } //holds a light animator
   
    void Start()
    {
        lightComponent = GetComponent<Light2D>();
    }
    public void Animate()
    {

    }
}
