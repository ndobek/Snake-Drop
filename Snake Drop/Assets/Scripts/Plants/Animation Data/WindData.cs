using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant Data/Wind Data")]
public class WindData : ScriptableObject
{
    public float gustFrequency;
    public float gustStrengthLoopFrequency;
    public float quiverFrequency;
    public float quiverFrequencyFrequency;
    public float quiverFrequencyFrequencyFrequency;
    public float mediumTreePeriod;

    public float MediumTreeCycle( float amplitude)
    {
        float result;
        result = MovementWave.AccessTriangleWave(amplitude, mediumTreePeriod);
        return result;
    }

    public float Gust(float latitude)
    {
        float result;
        result = MovementWave.AccessMovementWave(gustFrequency, latitude);
        return result;
    }
    public float Quiver(float latitude)
    {
        float result;
        result = MovementWave.AccessComplexMovementWave(quiverFrequency, quiverFrequencyFrequency, quiverFrequencyFrequencyFrequency, latitude);
        return result;
    }

    public float GustStrength()
    {
        float result;
        result = MovementWave.AccessMovementWave(gustStrengthLoopFrequency);
        return result;
    }
}
