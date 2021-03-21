using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Presets/Weather Type")]
public class WeatherPreset : ScriptableObject
{
    private string label;
    public string Label { get => label; }

    public List<WeatherVariant> weatherVariants;
    public EnvironmentalState environmentalState;
}
