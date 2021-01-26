using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public interface ILightPreset
{
    //holds all the light controlling properties so you can lerp to them
    Vector3 TransformPosition { get;}
    Vector3 TransformRotation { get; }
    Vector3 TransformScale { get; }
    Color32 Colour { get; }
    float Intensity { get; }
    float FalloffIntensity { get;}
    float InnerRadius { get; }
    float OuterRadius { get;}
    float InnerAngle { get;}
    float OuterAngle { get; }
    bool AlphaBlendOnOverlap { get; }
    bool UseNormalMap { get;}
    float VolumeOpacity { get;}
    float ShadowIntensity { get;}
    float ShadowVolumeInstensity { get;}
}


