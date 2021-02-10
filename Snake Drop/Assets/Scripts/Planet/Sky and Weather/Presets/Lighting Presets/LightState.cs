using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[System.Serializable]
public class LightState 
{


    #region Transform
    [SerializeField]
    private Vector3 transformPosition;
    public Vector3 TransformPosition { get { return transformPosition; } set { transformPosition = value; } }
    [SerializeField]
    private Vector3 transformRotation;
    public Vector3 TransformRotation { get { return transformRotation; } set { transformRotation = value; } }
    [SerializeField]
    private Vector3 transformScale;
    public Vector3 TransformScale { get { return transformScale; } set { transformScale = value; } }
    #endregion
    #region Angle and Radius
    [SerializeField]
    private float innerAngle;
    public float InnerAngle { get { return innerAngle; } set { innerAngle = value; } }
    [SerializeField]
    private float outerAngle;
    public float OuterAngle { get { return outerAngle; } set { outerAngle = value; } }
    [SerializeField]
    private float innerRadius;
    public float InnerRadius { get { return innerRadius; } set { innerRadius = value; } }
    [SerializeField]
    private float outerRadius;
    public float OuterRadius { get { return outerRadius; } set { outerRadius = value; } }
    #endregion
    #region Falloff, Blend mode, Colour, and Intensity
    [SerializeField]
    //private float falloffIntensity;
    //public float FalloffIntensity { get { return falloffIntensity; } set { falloffIntensity = value; } }
    //[SerializeField]
    //private bool alphaBlendOnOverlap;
    //public bool AlphaBlendOnOverlap { get { return alphaBlendOnOverlap; } set { alphaBlendOnOverlap = value; } }
    //[SerializeField]
    private Color32 colour;
    public Color32 Colour { get { return colour; } set { colour = value; } }
    [SerializeField]
    private float intensity;
    public float Intensity { get { return intensity; } set { intensity = value; } }
    #endregion
    #region Normal Map, Volume Opacity, Shadow Intensity, Shadow Volume

   // [SerializeField]
    //private bool useNormalMap;
    //public bool UseNormalMap { get { return useNormalMap; } set { useNormalMap = value; } }
    //[SerializeField]
    //private float distance;
    //public float Distance { get { return distance; } set { distance = value; } }
    //[SerializeField]
    //private float volumeOpacity;
    //public float VolumeOpacity { get { return volumeOpacity; } set { volumeOpacity = value; } }
    [SerializeField]
    private float shadowIntensity;
    public float ShadowIntensity { get { return shadowIntensity; } set { shadowIntensity = value; } }
    [SerializeField]
    private float shadowVolumeIntensity;
    public float ShadowVolumeIntensity { get { return shadowVolumeIntensity; } set { shadowVolumeIntensity = value; } }
    #endregion


}
