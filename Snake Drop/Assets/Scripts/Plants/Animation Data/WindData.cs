using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindData : MonoBehaviour
{
    public float gustFrequency;
    public float gustStrengthLoopFrequency;
    public float quiverFrequency;
    public float quiverFrequencyFrequency;
    public float quiverFrequencyFrequencyFrequency;

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
