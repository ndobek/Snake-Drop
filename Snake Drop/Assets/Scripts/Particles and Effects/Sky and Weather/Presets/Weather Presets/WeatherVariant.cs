using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Presets/Weather Variant")]
[System.Serializable]
public class WeatherVariant : ScriptableObject
{
    private string label;
    public string Label { get => label; }
    public WeatherState state;
}

