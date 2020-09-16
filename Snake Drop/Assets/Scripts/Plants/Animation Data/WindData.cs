using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant Data/Wind Data")]
public class WindData : ScriptableObject
{
    public float gustLengthLoopPeriod;
    public float gustLengthLoopAmplitude;
    public float gustLengthLoopMin;
    public float gustStrengthLoopPeriod;
    public float gustStrengthLoopAmplitude;
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
        result = MovementWave.AccessMovementWave(GustLength(), latitude);
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
        result = MovementWave.AccessMovementWave(gustStrengthLoopPeriod, 0, gustStrengthLoopAmplitude, gustStrengthLoopAmplitude + 1);
        return result;
    }
    public float GustLength()
    {
        float result;
        result = MovementWave.AccessMovementWave(gustLengthLoopPeriod, 0, gustLengthLoopAmplitude, gustLengthLoopAmplitude + gustLengthLoopMin);
        return result;
    }
}
