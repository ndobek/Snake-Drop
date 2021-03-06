using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeatherCycle : MonoBehaviour, IEffectAnimator<Weather, WeatherPreset, WeatherPreset>, ICyclical
{

    //WeatherAnimator deals with weather presets, which are lists of weather variants. Weather deals with weather variants.

    public Cycler cycler;
    public Weather weather;

    [SerializeField]
    private float cycleLength;
    public float CycleLength { get => cycleLength; set { cycleLength = value; } }

    private float cyclePoint = 0;
    public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }

    public List<WeatherPreset> weatherPresets;
    [SerializeField]
    private WeatherPreset initialState;
    public WeatherPreset InitialState { get => initialState; }

    private WeatherPreset previousState;
    public WeatherPreset PreviousState { get => previousState; set { previousState = value; } }

    public void TransitionComplete(WeatherPreset stateTransitionedTo, WeatherPreset stateTransitionedFrom)
    {
        currentState = stateTransitionedTo;
        previousState = stateTransitionedFrom;
    }


    private WeatherPreset currentState;
    public WeatherPreset CurrentState { get => currentState; set { currentState = value; } }

    private WeatherPreset RandWeather(List<WeatherPreset> presets)
    {
        WeatherPreset result = presets[Random.Range(0, presets.Count)];
        return result;
    }
    public void ChangeWeather()
    {
        WeatherPreset randWeather = RandWeather(weatherPresets);
        Debug.Log("Trying to change the weather to " + randWeather.weatherType + "...");
        Animate(currentState, randWeather, 1);
    }

    private void Start()
    {
        cycler.cyclicalBehaviours.Add(this);
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

    public void CycleUpdate()
    {
        cyclePoint += Time.deltaTime;
        if (cyclePoint >= cycleLength)
        {
            ChangeWeather();
            cyclePoint = 0f;
        }
    }
    
}
