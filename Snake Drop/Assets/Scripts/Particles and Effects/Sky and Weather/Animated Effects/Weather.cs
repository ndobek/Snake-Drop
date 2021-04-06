using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour, ICyclical
{

    #region Before Queue
    ////Different Light Presets
    ////Different Sky Presets
    //// Randomizes Them 
    ////Sets them    
    ////WeatherState comes from WeatherAnimator
    //public Cycler cycler;

    //public LightAnimator sunVolume;
    //public LightAnimator sunTerrain;
    //public LightAnimator skyVolume;
    //public LightAnimator skyDetail;
    ///// public SkyAnimator sky;

    //public VolumeAnimator volume;

    //public WeatherPreset initialPreset;
    //public WeatherState initialState;


    //public WeatherPreset currentPreset;
    //public WeatherPreset previousPreset;
    //public WeatherState currentState;
    //public WeatherState previousState;

    //private float cyclePoint = 0;
    //public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }
    //public float cycleLength;
    //public float CycleLength { get => cycleLength; set { cycleLength = value; } }

    //public float transitionPoint = 0;
    //public float transitionDuration;

    //public float TransitionPercent()
    //{
    //    return transitionPoint / transitionDuration;
    //}

    //public void SetWeather(WeatherPreset newWeather)
    //{
    //    previousPreset = currentPreset;
    //    currentPreset = newWeather;

    //    Debug.Log("Changing weather to _" + currentPreset.weatherType);
    //}

    //public void WeatherVariantChange()
    //{




    //    previousState = currentState;
    //    int index = Random.Range(0, currentPreset.weatherVariants.Count);
    //    currentState = currentPreset.weatherVariants[index].weatherState;

    //    Debug.Log("Changing "+ currentPreset.weatherType + " effects to variant " + index);
    //    cyclePoint = 0f;
    //    transitionPoint = 0f;


    //}




    //public void AnimateEffects()
    //{
    //   // sunVolume.Animate(previousState.sunVolumePreset.lightState, currentState.sunVolumePreset.lightState, TransitionPercent());
    //    //sunTerrain.Animate(previousState.sunTerrainPreset.lightState, currentState.sunTerrainPreset.lightState, TransitionPercent());
    //    //skyVolume.Animate(previousState.skyVolumePreset.lightState, currentState.skyVolumePreset.lightState, TransitionPercent());
    //   // skyDetail.Animate(previousState.skyDetailPreset.lightState, currentState.skyDetailPreset.lightState, TransitionPercent());

    //    volume.Animate(previousState.volumePreset.volumeState, currentState.volumePreset.volumeState, TransitionPercent());
    //}
    //public void TransitionComplete()
    //{

    //    //sunVolume.CurrentState = currentState.sunVolumePreset.lightState;
    //    //sunTerrain.CurrentState = currentState.sunTerrainPreset.lightState;
    //   // skyVolume.CurrentState = currentState.skyVolumePreset.lightState;
    //   // skyDetail.CurrentState = currentState.skyDetailPreset.lightState;
    //    volume.TransitionComplete(previousState.volumePreset.volumeState, currentState.volumePreset.volumeState);

    //}
    //public bool midTransition = false;
    //public void CycleUpdate()
    //{
    //    cyclePoint += Time.deltaTime;
    //    if (!midTransition && cyclePoint >= cycleLength)
    //    {
    //        WeatherVariantChange();
    //        midTransition = true;
    //        cyclePoint = 0f;
    //        transitionPoint = 0f;

    //    }
    //    if (midTransition)
    //    {
    //        transitionPoint += Time.deltaTime;
    //        AnimateEffects();
    //        if (TransitionPercent() >= 1 | cyclePoint >= cycleLength)
    //        {
    //            midTransition = false;
    //            TransitionComplete();
    //        }
    //    }
    //}
    //private void Awake()
    //{

    //    previousState = initialState;
    //    currentState = initialState;
    //    currentPreset = initialPreset;
    //    previousPreset = initialPreset;
    //    cycler.cyclicalBehaviours.Add(this);
    //}
    #endregion

    public Cycler cycler;


    public Queue<WeatherPreset> queuedPresets = new Queue<WeatherPreset>();


    public Queue<WeatherState> queuedStates = new Queue<WeatherState>();
    public WeatherState targetState;
    public WeatherState startingState;
    public WeatherPreset startingPreset;
    public List<IEffectAnimator<EnvironmentAnimator, EnvironmentalState, EnvironmentalState>> weatherReactions = new List<IEffectAnimator<EnvironmentAnimator, EnvironmentalState, EnvironmentalState>>();
    public VolumeAnimator volumeAnimator;
    [SerializeField]
    private float cycleLength;
    public float CycleLength { get => cycleLength; set { cycleLength = value; } }
    private float cyclePoint = 0;
    public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }

    
    public bool MidCycle()
    {
        return (cyclePoint < cycleLength);
    }
    public bool MidTransition()
    {
        return (transitionPoint < transitionLength);
    }
    private float transitionPoint;
    public float transitionLength;
    public float TransitionPercent()
    {
        return (transitionPoint / transitionLength);
    }
    public void WeatherVariantChange()
    {
        int randIndex = Random.Range(0, (startingPreset.weatherVariants.Count));
        queuedStates.Enqueue(startingPreset.weatherVariants[randIndex].state);
        Debug.Log("Changing " + startingPreset.Label + " effects to variant " + randIndex);
        

    }
    public void SetWeather(WeatherPreset newWeather)
    {
        queuedPresets.Enqueue(newWeather);
        
        Debug.Log("Adding Forecast: "+ newWeather.Label);
        foreach (IEffectAnimator<EnvironmentAnimator, EnvironmentalState, EnvironmentalState> reaction in weatherReactions)
        {
            EnvironmentalState oldState = reaction.CurrentState;
            reaction.Animate(oldState, newWeather.environmentalState, 1f);
            
        }
    }
    private void TransitionComplete()
    {
        volumeAnimator.TransitionComplete(targetState.volumePreset.volumeState, startingState.volumePreset.volumeState);
        
    }
    public void CycleUpdate()
    {
        cyclePoint += Time.deltaTime;
        if (!MidCycle() && !MidTransition())
        {
            WeatherVariantChange();
        }
        if (MidTransition())
        {
            Animate();
        }
        else if (queuedStates.Count > 0)
        {
            startingState = targetState;
            targetState = queuedStates.Dequeue();
            transitionPoint = 0;
        }
        

    }
    public void Animate()
    {
        transitionPoint += Time.deltaTime;
        volumeAnimator.Animate(startingState.volumePreset.volumeState, targetState.volumePreset.volumeState, TransitionPercent());
        if (!MidTransition())
        {
            TransitionComplete();
        }
    }

    private void Awake()
    {
        targetState = startingState;
        cycler.cyclicalBehaviours.Add(this);
    }

    
}
