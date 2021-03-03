using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Presets/Weather Type")]
public class WeatherPreset: ScriptableObject
{
    public string weatherType;
    public List<WeatherVariant> weatherVariants;
}
