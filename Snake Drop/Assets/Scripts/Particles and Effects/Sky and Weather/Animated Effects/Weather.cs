using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour, ICyclical
{
    //Different Light Presets
    //Different Sky Presets
    // Randomizes Them 
    //Sets them    
    //WeatherState comes from WeatherAnimator
    public Cycler cycler;

    public LightAnimator sunVolume;
    public LightAnimator sunTerrain;
    public LightAnimator skyVolume;
    public LightAnimator skyDetail;
   /// public SkyAnimator sky;

    public WeatherPreset currentPreset;
    public WeatherState currentState;

    private float cyclePoint = 0;
    public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }
    public float cycleLength;
    public float CycleLength { get => cycleLength; set { cycleLength = value; } }

    public float weatherTransitionDuration;
    private float weatherTransitionPercent = 0;

    public void SetWeather(WeatherPreset newWeather)
    {
        currentPreset = newWeather;
        
        Debug.Log("Changing weather to _" + currentPreset.weatherType);
    }
   
    public void WeatherVariantChange()
    {
        int index = Random.Range(0, currentPreset.weatherVariants.Count);
        currentState = currentPreset.weatherVariants[index].weatherState;

        Debug.Log("Changing "+ currentPreset.weatherType + " effects to variant " + index);
        cyclePoint = 0f;
        weatherTransitionPercent = 0f;
    }




    public void AnimateEffects()
    {
        sunVolume.Animate(sunVolume.CurrentState, currentState.sunVolumePreset.lightState, cyclePoint/weatherTransitionDuration );
        sunTerrain.Animate(sunVolume.CurrentState, currentState.sunTerrainPreset.lightState, cyclePoint/weatherTransitionDuration);
        skyVolume.Animate(skyVolume.CurrentState, currentState.skyVolumePreset.lightState, cyclePoint/weatherTransitionDuration);
        skyDetail.Animate(skyDetail.CurrentState, currentState.skyDetailPreset.lightState, cyclePoint/weatherTransitionDuration);
        //sky.Animate(sky.CurrentState, currentState.skyPreset.skyState, cyclePoint/weatherTransitionDuration);
    }
    public void TransitionComplete()
    {
        sunVolume.CurrentState = currentState.sunVolumePreset.lightState;
        sunTerrain.CurrentState = currentState.sunTerrainPreset.lightState;
        skyVolume.CurrentState = currentState.skyVolumePreset.lightState;
        skyDetail.CurrentState = currentState.skyDetailPreset.lightState;
        //sky.CurrentState = currentState.skyPreset.skyState;
    }
    bool midTransition = false;
    public void CycleUpdate()
    {
        cyclePoint += Time.deltaTime;
        if (!midTransition && cyclePoint > cycleLength)
        {
            WeatherVariantChange();
            midTransition = true;
            cyclePoint = 0;          
        }
        else if (midTransition)
        {
            weatherTransitionPercent = Mathf.Lerp(weatherTransitionPercent, 1, cyclePoint /weatherTransitionDuration);
            AnimateEffects();
            if (weatherTransitionPercent >= 1)
            {
                midTransition = false;
                TransitionComplete();
            }
        }
    }
    private void Start()
    {
        cycler.cyclicalBehaviours.Add(this);
    }
}
