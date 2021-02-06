using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimatableLight
{
    
    public List<LightPreset> lightPresets;

    public GameObject lightAnimator;
    

    //Holds a list of light presets and a light animator. This keeps the right presets with the right light.
    //This is meant to be used by weathers
}
