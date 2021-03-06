using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


//This class:
//Goes on light
//Interpolates data from keyframe
//Instantiates a frame
//Updates the light using data from the new frame
public class LightAnimator : MonoBehaviour, IEffectAnimator<Light2D, LightState, LightPreset>
{
    //renamed light to lightComponent because unity had an issue with the name light, probably because it's already a thing
    public Light2D lightComponent;
    [SerializeField]
    private LightPreset initialState;
    public LightPreset InitialState { get => initialState; }

    private LightState previousState;
    public LightState PreviousState { get => previousState; set { previousState = value; } }

    private LightState currentState;
    public LightState CurrentState { get => currentState; set { currentState = value; } }
    private void Awake()
    {
        if (lightComponent == null) lightComponent = GetComponent<Light2D>();
        currentState = initialState.lightState;
    }

    public void UpdateEffect(Light2D effect, LightState nextFrame)
    {
        effect.pointLightInnerAngle = nextFrame.InnerAngle;
        effect.pointLightOuterAngle = nextFrame.OuterAngle;
        effect.pointLightInnerRadius = nextFrame.InnerRadius;
        effect.pointLightOuterRadius = nextFrame.OuterRadius;
        //effect.falloffIntensity = nextFrame.FalloffIntensity; //read only :(
        //effect.alphaBlendOnOverlap = nextFrame.AlphaBlendOnOverlap;
        effect.color = nextFrame.Colour;
        effect.intensity = nextFrame.Intensity;
        //effect.volumeOpacity = nextFrame
        effect.shadowIntensity = nextFrame.ShadowIntensity;
        effect.shadowVolumeIntensity = nextFrame.ShadowVolumeIntensity;

    }

    public void Animate(LightState keyframe1, LightState keyframe2, float t)
    {
        LightState result = new LightState();
        result.InnerAngle = Mathf.Lerp(keyframe1.InnerAngle, keyframe2.InnerAngle, t);
        result.OuterAngle = Mathf.Lerp(keyframe1.OuterAngle, keyframe2.OuterAngle, t);
        result.InnerRadius = Mathf.Lerp(keyframe1.InnerRadius, keyframe2.InnerRadius, t);
        result.OuterRadius = Mathf.Lerp(keyframe1.OuterRadius, keyframe2.OuterRadius, t);
        result.Colour = Color.Lerp(keyframe1.Colour, keyframe2.Colour, t);
        result.Intensity = Mathf.Lerp(keyframe1.Intensity, keyframe2.Intensity, t);
        result.ShadowIntensity = Mathf.Lerp(keyframe1.ShadowIntensity, keyframe2.ShadowIntensity, t);
        result.ShadowVolumeIntensity = Mathf.Lerp(keyframe1.ShadowVolumeIntensity, keyframe2.ShadowVolumeIntensity, t);

       
        UpdateEffect(lightComponent, result);

        
    }
    public void TransitionComplete(LightState transitionedTo, LightState transitionedFrom)
    {
        previousState = transitionedFrom;
        currentState = transitionedTo;
    }
}
