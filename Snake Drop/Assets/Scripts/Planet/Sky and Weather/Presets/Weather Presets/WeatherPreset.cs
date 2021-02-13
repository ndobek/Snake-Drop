using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeatherPreset: ScriptableObject
{
    public string weatherType;
    public List<WeatherVariant> weatherVariants;
}
