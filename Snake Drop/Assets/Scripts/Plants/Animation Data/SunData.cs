using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant Data/Sun Data")]
public class SunData : ScriptableObject
{

    public float sunOrbitalVelocity = 1;

    public float SunZenith(float latitude)
    {
        float result;
        result = MovementWave.AccessMovementWave(sunOrbitalVelocity, latitude);
        return result;
    }

}
