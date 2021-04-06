using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour, ICyclical
{
    public EnvironmentAnimator animator;
    public WeatherCycle weatherCycle;
    private int intensityLevel = 0;
    public int IntensityLevel { get => intensityLevel; }
    private EnvironmentalState currentState;
    private List<Intensity> intensityLevels = new List<Intensity>();

    private float cycleLength;
    public float CycleLength { get => cycleLength; set { cycleLength = value; } }
    private float cyclePoint;
    public float CyclePoint { get => cyclePoint; set { cyclePoint = value; } }

    
   
   
    private void Start()
    {
        animator.effects.Add(this);
       
    }


    public void SetState(EnvironmentalState state)
    {
        intensityLevels.Clear();
        currentState = state;
        foreach (Intensity intensity in currentState.intensities)
        {
            for (int i = 0; i < intensity.weight; i++)
            {
                intensityLevels.Add(intensity);
                
            }
        }
    }

    public void SetIntensity()
    {
        if (intensityLevels != null)
        {
            int randIndex = Random.Range(0, intensityLevels.Count - 1);
            intensityLevel = intensityLevels[randIndex].intensityValue;
            cycleLength = Random.Range(intensityLevels[randIndex].minimumDuration, intensityLevels[randIndex].maximumDuration);
        }

    }
    public void CycleUpdate()
    {
        if (CyclePoint >= CycleLength)
        {
            SetIntensity();
            CyclePoint = 0f;
        }
        CyclePoint += Time.deltaTime;
        
    }
}
  

  
    
