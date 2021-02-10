using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAnimator : MonoBehaviour, IEffectAnimator<Weather, WeatherState>
{
    public Weather weather;

    public List<WeatherPreset> weatherPresets;

    private WeatherState currentState;
    public WeatherState CurrentState { get => currentState; }

    private WeatherPreset RandWeather(List<WeatherPreset> presets)
    {
        WeatherPreset result = presets[Random.Range(0, presets.Count)];
        return result;
    }
    public string currentWeatherType;
    public void AChangeInTheWeather()
    {
        WeatherPreset randWeather = RandWeather(weatherPresets);
        currentWeatherType = randWeather.weatherTypeName;
        Debug.Log("okay so we know the event callback works thank GOODNESS");
        Debug.Log("Trying to change the weather to " + currentWeatherType + "now...");
        Animate(currentState, randWeather.weatherState, 1);
    }

    private void Start()
    {
        weather = gameObject.GetComponent<Weather>();

        Animate(weatherPresets[1].weatherState, weatherPresets[1].weatherState, 1);



    }

    public void Animate(WeatherState keyframe1, WeatherState keyframe2, float t)
    {
        WeatherState result = new WeatherState();
        currentState = result;
        UpdateEffect(weather, keyframe2);

    }
    
    public void UpdateEffect(Weather effect, WeatherState nextFrame)
    {
        //sets the weatherstate
        effect.SetWeather(nextFrame, currentWeatherType);
    }
    private void Update()
    {
        weather.WeatherUpdate();

    }

}
