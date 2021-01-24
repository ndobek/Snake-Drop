using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeatherPreset : ScriptableObject, IWeatherPreset, IPresetData
{
    public List<IAnimatableEffect> Effects { get; }

    public void returnPresetData()
    {

    }
}
