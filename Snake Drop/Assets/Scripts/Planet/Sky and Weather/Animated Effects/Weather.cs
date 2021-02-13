using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    //Different Light Presets
    //Different Sky Presets
    // Randomizes Them 
    //Sets them    
    //WeatherState comes from WeatherAnimator
    public LightAnimator sunVolume;
    public LightAnimator sunTerrain;
    public LightAnimator skyVolume;
    public LightAnimator skyDetail;
    public SkyAnimator sky;

    public WeatherPreset currentPreset;
    public WeatherState currentState;



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
        weatherVariantTime = 0f;
        weatherTransitionPercent = 0f;
    }
    private float weatherVariantTime = 0;
    public float weatherTransitionDuration;
    public float weatherVariantDuration;
    private float weatherTransitionPercent = 0;

    public void AnimateEffects()
    {
        sunVolume.Animate(sunVolume.CurrentState, currentState.sunVolumePreset.lightState, weatherVariantTime/weatherTransitionDuration );
        sunTerrain.Animate(sunVolume.CurrentState, currentState.sunTerrainPreset.lightState, weatherVariantTime/weatherTransitionDuration);
        skyVolume.Animate(skyVolume.CurrentState, currentState.skyVolumePreset.lightState, weatherVariantTime/weatherTransitionDuration);
        skyDetail.Animate(skyDetail.CurrentState, currentState.skyDetailPreset.lightState, weatherVariantTime/weatherTransitionDuration);
        sky.Animate(sky.CurrentState, currentState.skyPreset.skyState, weatherVariantTime/weatherTransitionDuration);
    }
    public void TransitionComplete()
    {
        sunVolume.CurrentState = currentState.sunVolumePreset.lightState;
        sunTerrain.CurrentState = currentState.sunTerrainPreset.lightState;
        skyVolume.CurrentState = currentState.skyVolumePreset.lightState;
        skyDetail.CurrentState = currentState.skyDetailPreset.lightState;
        sky.CurrentState = currentState.skyPreset.skyState;
    }
    bool midTransition = false;
    public void WeatherUpdate()
    {
        weatherVariantTime += Time.deltaTime;
        if (!midTransition && weatherVariantTime > weatherVariantDuration)
        {
            WeatherVariantChange();
            midTransition = true;
            weatherVariantTime = 0;          
        }
        else if (midTransition)
        {
            weatherTransitionPercent = Mathf.Lerp(weatherTransitionPercent, 1, weatherVariantTime /weatherTransitionDuration);
            AnimateEffects();
            if (weatherTransitionPercent >= 1)
            {
                midTransition = false;
                TransitionComplete();
            }
        }
    }
}
