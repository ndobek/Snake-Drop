using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[System.Serializable]
public class AnimatableLight
{
    
    public List<LightPreset> lightPresets;

    [SerializeField]
    private IEffectAnimator<Light2D, LightPreset, LightPreset> lightAnimator;
    public IEffectAnimator<Light2D, LightPreset, LightPreset> LightAnimator { get; }
    

    //Holds a list of light presets and a light animator. This keeps the right presets with the right light.
    //This is meant to be used by weathers
}
