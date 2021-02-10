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
    //public List<SunVolumeLightPreset> sunVolumePresets;
    //public List<SunTerrainLightPreset> sunTerrainPresets;
    //public List<SkyVolumeLightPreset> skyVolumePresets;
    //public List<SkyDetailLightPreset> skyDetailPresets;

    public float effectChangeSpeed;
    //TODO: think about how this number should be determined

    //private int SelectRandIndex<preset>(List<preset> presetBank)
    //{
    //    int randIndex = Random.Range(0, presetBank.Count - 1);
    //    return randIndex;
    //}
    public void SetWeather(WeatherPreset newWeather)
    {
        currentPreset = newWeather;
        
        Debug.Log("Changing weather to _" + currentPreset.weatherType);
    }
   
    public void WeatherStateChange()
    {
        int index = Random.Range(0, 1 - currentPreset.weatherStates.Count);
        currentState = currentPreset.weatherStates[index];

        Debug.Log("Changing "+ currentPreset.weatherType + "effects to " + index);
        weatherStateTime = 0f;
        weatherShiftPercent = 0f;
    }
    public float weatherStateTime = 0;
    public float weatherStateDuration;
    public float weatherShiftPercent = 0;

    public void AnimateLights()
    {
        sunVolume.Animate(sunVolume.CurrentState, currentState.sunVolumePreset.lightState, effectChangeSpeed);
        sunTerrain.Animate(sunVolume.CurrentState, currentState.sunTerrainPreset.lightState, effectChangeSpeed);
        skyVolume.Animate(skyVolume.CurrentState, currentState.skyVolumePreset.lightState, effectChangeSpeed);
        skyDetail.Animate(skyDetail.CurrentState, currentState.skyDetailPreset.lightState, effectChangeSpeed);
        sky.Animate(sky.CurrentState, currentState.skyPreset.skyState, effectChangeSpeed);
    }
    public void WeatherUpdate()
    {
        //Todo: fix logic. this should change the weather if it's been enough time and it's not mid-transition, and animate the weather if it is mid-transition,
        //and do nothing if it's neither. (except increment time.) time should get incremented for everything except make sure it gets reset when the weather changes.
        if (weatherStateTime >= weatherStateDuration)
        {
            if (weatherShiftPercent >= 1)
            {
                WeatherStateChange();
            }
        }
        else
        {
            weatherShiftPercent += Mathf.Lerp(weatherShiftPercent, 1, effectChangeSpeed);

            AnimateLights();
        }
            

        weatherStateTime += Time.deltaTime;





    }





}
