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
    public static float AccessSawtooth(float rate = 1, float range = 2)
    {
        float timeMod = (Time.time * rate) % range;
        return timeMod;
    }

    public static float AccessTriangleWave(float amplitude, float period)
    {
        float result = (2 * amplitude / (float)Math.PI) * (float)Math.Asin((float)Math.Sin(2 * ((float)Math.PI / period) * Time.time));
        return result;
    }
    // Replace this function because you can't control the period
    //public static float AccessTriangleWave(float rate = 1, float range = 2)
    //{
    //    float movementValue = AccessSawtooth(rate, range);
    //    movementValue = Math.Abs(movementValue - (range / 2));
    //    return movementValue - (range/4) ;
    //}
    



}
