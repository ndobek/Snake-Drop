using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MovementWave
{

    //This function gives you the value the animation state will be based on for the plant movement.
    public static float AccessMovementWave(float rate = 1, float offset = 0)
    {
        float movementValue = (float)Math.Sin(Time.time * rate + offset);
        return movementValue;
    }
    public static float AccessComplexMovementWave(float rate1 = 1, float rate2 = 1, float rate3 = 1, float offset = 0)
    {
        float movementValue = (float)Math.Sin(rate1 * (float)Math.Sin(rate2 * (float)Math.Sin( rate3 * Time.time + offset)));
        return movementValue;
    }




}
