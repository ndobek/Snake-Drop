using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Sun : ScriptableObject
{

    public float sunOrbitalVelocity = 1;

    public float SunZenith(float latitude)
    {
        float result;
        result = MovementWave.AccessMovementWave(sunOrbitalVelocity, latitude);
        return result;
    }

}
