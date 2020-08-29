using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SunData : MonoBehaviour
{

    public float sunOrbitalVelocity = 1;

    public float SunZenith(float latitude)
    {
        float result;
        result = MovementWave.AccessMovementWave(sunOrbitalVelocity, latitude);
        return result;
    }

}
