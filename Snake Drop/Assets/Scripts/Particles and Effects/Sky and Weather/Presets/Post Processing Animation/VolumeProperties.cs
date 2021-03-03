using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VolumeProperties
{
    private float testProperty;
    public float TestProperty { get => testProperty; set { testProperty = value; } }
}
