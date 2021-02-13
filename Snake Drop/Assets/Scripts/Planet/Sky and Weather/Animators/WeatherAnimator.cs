using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeatherAnimator : MonoBehaviour, IEffectAnimator<Weather, WeatherPreset, WeatherPreset>
{

    //WeatherAnimator deals with presets, which are lists of states. Weather deals with states.
    //The states in this one are presets
    public Weather weather;

    public List<WeatherPreset> weatherPresets;
    [SerializeField]
    private WeatherPreset initialState;
    public WeatherPreset InitialState { get => initialState; }

    private WeatherPreset currentState;
    public WeatherPreset CurrentState { get => currentState; set { currentState = value; } }

    private WeatherPreset RandWeather(List<WeatherPreset> presets)
    {
        WeatherPreset result = presets[Random.Range(0, presets.Count)];
        return result;
    }
    public void AChangeInTheWeather()
    {
        WeatherPreset randWeather = RandWeather(weatherPresets);
        Debug.Log("okay so we know the event callback works thank GOODNESS");
        Debug.Log("Trying to change the weather to " + randWeather.weatherType + "now...");
        Animate(currentState, randWeather, 1);
    }

    private void Start()
    {
        weather = gameObject.GetComponent<Weather>();

        Animate(initialState, initialState, 1);

    }

    public void Animate(WeatherPreset keyframe1, WeatherPreset keyframe2, float t)
    {
        currentState = keyframe2;
        UpdateEffect(weather, keyframe2);
    }
    
    public void UpdateEffect(Weather effect, WeatherPreset nextFrame)
    {
        //sets the weatherPreset
        effect.SetWeather(nextFrame);
    }
    private void Update()
    {
        weather.WeatherUpdate();
    }

}
