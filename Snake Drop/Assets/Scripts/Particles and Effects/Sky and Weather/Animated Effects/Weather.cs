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

    public VolumeAnimator volume;

    public WeatherPreset initialPreset;
    public WeatherState initialState;

    public WeatherPreset currentPreset;
    public WeatherPreset previousPreset;
    public WeatherState currentState;
    public WeatherState previousState;

    private float cyclePoint = 0;
    public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }
    public float cycleLength;
    public float CycleLength { get => cycleLength; set { cycleLength = value; } }

    public float transitionPoint = 0;
    public float transitionDuration;

    public float TransitionPercent()
    {
        return transitionPoint / transitionDuration;
    }

    public void SetWeather(WeatherPreset newWeather)
    {
        previousPreset = currentPreset;
        currentPreset = newWeather;
        
        Debug.Log("Changing weather to _" + currentPreset.weatherType);
    }
   
    public void WeatherVariantChange()
    {
        
        previousState = currentState;
        int index = Random.Range(0, currentPreset.weatherVariants.Count);
        currentState = currentPreset.weatherVariants[index].weatherState;

        Debug.Log("Changing "+ currentPreset.weatherType + " effects to variant " + index);
        cyclePoint = 0f;
        transitionPoint = 0f;
        
        
    }




    public void AnimateEffects()
    {
       // sunVolume.Animate(previousState.sunVolumePreset.lightState, currentState.sunVolumePreset.lightState, TransitionPercent());
        //sunTerrain.Animate(previousState.sunTerrainPreset.lightState, currentState.sunTerrainPreset.lightState, TransitionPercent());
        //skyVolume.Animate(previousState.skyVolumePreset.lightState, currentState.skyVolumePreset.lightState, TransitionPercent());
       // skyDetail.Animate(previousState.skyDetailPreset.lightState, currentState.skyDetailPreset.lightState, TransitionPercent());
        
        volume.Animate(previousState.volumePreset.volumeState, currentState.volumePreset.volumeState, TransitionPercent());
    }
    public void TransitionComplete()
    {
        
        //sunVolume.CurrentState = currentState.sunVolumePreset.lightState;
        //sunTerrain.CurrentState = currentState.sunTerrainPreset.lightState;
       // skyVolume.CurrentState = currentState.skyVolumePreset.lightState;
       // skyDetail.CurrentState = currentState.skyDetailPreset.lightState;
        volume.TransitionComplete(previousState.volumePreset.volumeState, currentState.volumePreset.volumeState);
      
    }
    public bool midTransition = false;
    public void CycleUpdate()
    {
        cyclePoint += Time.deltaTime;
        if (!midTransition && cyclePoint >= cycleLength)
        {
            WeatherVariantChange();
            midTransition = true;
            cyclePoint = 0f;
            transitionPoint = 0f;
            
        }
        if (midTransition)
        {
            transitionPoint += Time.deltaTime;
            AnimateEffects();
            if (TransitionPercent() >= 1 | cyclePoint >= cycleLength)
            {
                midTransition = false;
                TransitionComplete();
            }
        }
    }
    private void Awake()
    {
        previousState = initialState;
        currentState = initialState;
        currentPreset = initialPreset;
        previousPreset = initialPreset;
        cycler.cyclicalBehaviours.Add(this);
    }
}
