using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightPreset : ScriptableObject, ILightPreset, IPresetData
{
    [SerializeField]
    private Vector3 transformPosition;
    public Vector3 TransformPosition { get { return transformPosition; } }
    [SerializeField]
    private Vector3 transformRotation;
    public Vector3 TransformRotation { get { return transformRotation; } }
    [SerializeField]
    private Vector3 transformScale;
    public Vector3 TransformScale { get { return transformScale; } }



    [SerializeField]
    private Color32 colour;
    public Color32 Colour { get { return colour; } }

    [SerializeField]
    private float intensity;
    public float Intensity { get { return intensity; } }
    [SerializeField]
    private float falloffIntensity;
    public float FalloffIntensity { get { return falloffIntensity; } }

    [SerializeField]
    private float innerRadius;
    public float InnerRadius { get { return innerRadius; } }
    [SerializeField]
    private float outerRadius;
    public float OuterRadius { get { return outerRadius; } }
    [SerializeField]
    private float innerAngle;
    public float InnerAngle { get { return innerAngle; } }
    [SerializeField]
    private bool alphaBlendOnOverlap;
    public bool AlphaBlendOnOverlap { get { return alphaBlendOnOverlap; } }
    [SerializeField]
    private bool useNormalMap;
    public bool UseNormalMap { get { return useNormalMap; } }
    [SerializeField]
    private float volumeOpacity;
    public float VolumeOpacity { get { return volumeOpacity; } }
    [SerializeField]
    private float shadowIntensity;
    public float ShadowIntensity { get { return shadowIntensity; } }
    [SerializeField]
    private float shadowVolumeIntensity;
    public float ShadowVolumeInstensity { get { return shadowVolumeIntensity; } }

    //TODO: make this something not useless
    public void returnPresetData()
    {

    }

}
