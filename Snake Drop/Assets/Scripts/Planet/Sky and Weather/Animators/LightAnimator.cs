using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightAnimator : MonoBehaviour, IEffectAnimator
{
    [SerializeField]
    private List<IAnimatableEffect> effects;
    public List<IAnimatableEffect> Effects { get { return effects; } }
   
    //This class shouldn't have the presetData, because how would it know which ones go with which light? 
    //instead, the light interface needs to have different methods you can call on it, like cool() but that sucks,
    //because you would have to add them to a billion things...it's better if you just add them here. that means you need
    //to also be able to set the light type, the cookie, the order, etc so that it can just cycle through a bunch of lights and
    //make them whatever they need to be.
    //[SerializeField]
    //private List<IPresetData> presetData;
    //public List<IPresetData> PresetData { get { return presetData; } }



   
    public void UpdateAnimations()
    {
    }
}
