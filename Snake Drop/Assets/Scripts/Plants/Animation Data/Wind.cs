using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Wind : ScriptableObject
{
    public float gustFrequency;
    public float gustStrengthLoopFrequency;

    public float Gust(float latitude)
    {
        float result;
        result = MovementWave.AccessMovementWave(gustFrequency, latitude);
        return result;
    }

    public float GustStrength(float latitude)
    {
        float result;
        result = MovementWave.AccessMovementWave(gustStrengthLoopFrequency, latitude);
        return result;
    }
}
